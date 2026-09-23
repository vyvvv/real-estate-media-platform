using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using RealEstateMediaPlatform.API.Models;
using System.IO.Compression;

namespace RealEstateMediaPlatform.API.Services.AzureBlobStorage
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;
        public AzureBlobStorageService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {

            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
        }

        //Upload File
        public async Task<string> UploadAsync(IFormFile file, string folder)
        {


            //Create a Container instance
            var containerClient = _blobServiceClient.GetBlobContainerClient(_configuration["AzureBlobStorage:Container"]);
            await containerClient.CreateIfNotExistsAsync();
            var blobName = $"{folder}/{file.FileName}";

            //Create a Blob instance
            var blobClient = containerClient.GetBlobClient(blobName);
            await using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
            //return blobClient.Uri.ToString();
            return GenerateSasUrl(blobClient, TimeSpan.FromDays(730));
        }

        //Download File
        public async Task<(Stream Content, string ContentType, string FileName)> DownloadAsync(string blobUrl)
        {
            var uri = new Uri(blobUrl);
            var fileName = uri.Segments.LastOrDefault()?.TrimStart('/')??"Download";
            var blobClient = new BlobClient(uri);
            var response = await blobClient.DownloadContentAsync();
            var blobContent = response.Value.Content.ToStream();
            var contentType = response.Value.Details.ContentType;
            return (blobContent, contentType, fileName);
        }


        // Generates a time-limited SAS (Shared Access Signature) URL for secure blob access
        private string GenerateSasUrl(BlobClient blobClient, TimeSpan duration)
        {
            // Verify that the blob client supports SAS URI generation
            if (!blobClient.CanGenerateSasUri)
            {
                throw new InvalidOperationException("Cannot generate SAS URI. Check your authentication method.");
            }

            // Configure SAS builder with security constraints
            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                BlobName = blobClient.Name,
                Resource = "b", // b = blob
                ExpiresOn = DateTimeOffset.UtcNow.Add(duration),
                Protocol = SasProtocol.Https
            };

            //Grant read-only permission
            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            return blobClient.GenerateSasUri(sasBuilder).ToString();
        }

        //Zip MediaAssets
        public async Task<MemoryStream?> ZipMediaAssetsAsync(List<MediaAsset> mediaAssets)
        {
            var zipStream = new MemoryStream();

            // Create ZIP archive
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true)) 
            { 
                var _mediaAssets = mediaAssets;

                foreach (var mediaAsset in _mediaAssets)
                {

                    var (fileStream, contentType, fileName) = await DownloadAsync(mediaAsset.MediaUrl);

                    var entryName = $"{mediaAsset.Id}_{fileName}";
                    var entry = archive.CreateEntry(entryName);// Add file to ZIP

                    using (fileStream)
                    using (var entryStream = entry.Open()) // Copy file data
                    {
                        await fileStream.CopyToAsync(entryStream);

                    }
                }
            }// ZIP finalized here
            zipStream.Position = 0;// Reset position
            return zipStream;
        }

    }
}
