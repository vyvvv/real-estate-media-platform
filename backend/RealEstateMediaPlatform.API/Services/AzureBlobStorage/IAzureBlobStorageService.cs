using RealEstateMediaPlatform.API.Models;

namespace RealEstateMediaPlatform.API.Services.AzureBlobStorage
{
    public interface IAzureBlobStorageService
    {
        Task<string> UploadAsync(IFormFile file, string folder);
        Task<(Stream Content, string ContentType, string FileName)> DownloadAsync(string blobUrl);
        Task<MemoryStream?> ZipMediaAssetsAsync(List<MediaAsset> mediaAssets);
    }
}
