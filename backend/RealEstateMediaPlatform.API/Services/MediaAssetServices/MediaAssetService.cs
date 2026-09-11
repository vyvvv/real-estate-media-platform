using RealEstateMediaPlatform.API.Enums;
using RealEstateMediaPlatform.API.Models;
using RealEstateMediaPlatform.API.Repositories.MediaAssetRepositories;
using RealEstateMediaPlatform.API.Services.AzureBlobStorage;
using RealEstateMediaPlatform.API.Exceptions;



namespace RealEstateMediaPlatform.API.Services.MediaAssetServices
{
    public class MediaAssetService : IMediaAssetService
    {

        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly IMediaAssetRepository _mediaAssetRepository;
        public MediaAssetService(IAzureBlobStorageService azureBlobStorageService, IMediaAssetRepository mediaAssetRepository)
        {

            _azureBlobStorageService = azureBlobStorageService;
            _mediaAssetRepository = mediaAssetRepository;
        }
        public async Task<List<int>> UploadMediaAssetsAsync(List<IFormFile> files, MediaAssetType type, int listingCaseId,string userId)
        {
            // Validate input parameters
            if (files == null || files.Count == 0)
            {
                throw new ArgumentException("No files provided");
            }

            //Check if mediaAssetType is valid enum value
            if (!Enum.IsDefined(typeof(MediaAssetType), type))
            {

                throw new ArgumentException($"Invalid MediaAssetType: {type}");
            }


            //Only Picture allows multiple uploads
            if (type != MediaAssetType.Picture && files.Count > 1)
            {

                throw new InvalidOperationException("Upload one file at a time for non-picture type");
            }
      
             var listingCase = await _mediaAssetRepository.GetValidListingCaseById(listingCaseId);

            if (listingCase == null) {
                throw new KeyNotFoundException($"listingCase not found with Id:{listingCaseId}");
            }

            Console.WriteLine($"listingCase.MediaAssets count before upload: {listingCase.MediaAssets?.Count ?? 0}");


            //File size & file type validation for each uploaded file
            foreach (var file in files)
            {
                // Validate empty file (0 bytes)
                if (file.Length == 0)
                    throw new Exception("File is empty");

                // Global file size limit (protect from DoS uploads)
                if (file.Length > 600 * 1024 * 1024) // 600MB max
                    throw new Exception("File too large (limit 600MB)");

                // ------------ Picture validation ------------
                if (type == MediaAssetType.Picture)
                {
                    var allowedImageTypes = new[]
                    {
                    "image/jpeg",
                    "image/png",
                    "image/webp"
                    };

                if (!allowedImageTypes.Contains(file.ContentType))
                    throw new Exception("Invalid image format. Allowed: JPG, PNG, WEBP");

                // Image size limit (10MB)
                if (file.Length > 10 * 1024 * 1024)
                    throw new Exception("Image too large (max 10MB)");
                }

                // ------------ Video validation ------------
                if (type == MediaAssetType.Video)
                {
                    var allowedVideoTypes = new[]
                    {
                    "video/mp4",
                    "video/quicktime",
                    "video/webm"
                    };

                if (!allowedVideoTypes.Contains(file.ContentType))
                    throw new Exception("Invalid video format. Allowed: MP4, MOV, WEBM");

                // Video size limit (500MB)
                if (file.Length > 500 * 1024 * 1024)
                    throw new Exception("Video too large (max 500MB)");
                }

                // ------------ FloorPlan validation ------------
                if (type == MediaAssetType.FloorPlan)
                {
                    var allowedFloorPlanTypes = new[]
                    {
                    "image/jpeg",
                    "image/png",
                    "application/pdf"
                    };

                if (!allowedFloorPlanTypes.Contains(file.ContentType))
                    throw new Exception("Invalid floor plan format. Allowed: JPG, PNG, PDF");

                // Floor plan size limit (20MB)
                if (file.Length > 20 * 1024 * 1024)
                    throw new Exception("Floor plan too large (max 20MB)");
                }

                //VR section closed at the moment

                if (type == MediaAssetType.VRTour)
                    throw new FeatureNotAvailableException("VRTour upload is not available yet.");
            }
         
            //END of new validation block
         

            //Process file uploads
            var mediaAssetsId = new List<int>();
            foreach (var file in files)
            {
                //Upload file to Azure Blob Storage
                var url_result = await _azureBlobStorageService.UploadAsync(file, "MediaAsset");
                if (string.IsNullOrEmpty(url_result))
                {
                    throw new InvalidOperationException($"Upload failed for file: {file.FileName}");
                }

                //Create new MediaAsset entity
                MediaAsset mediaAsset = new MediaAsset
                {

                    MediaType = type,
                    MediaUrl = url_result,
                    ListingCaseId = listingCase.Id,
                    UserId = userId,
                    UploadedAt = DateTime.UtcNow,
                    IsSelect = false,
                    IsHero = false,
                    IsDeleted = false

                };

                // Save media asset to database and collect ID
                var mediaId = await _mediaAssetRepository.CreateMediaAssets(mediaAsset);
                mediaAssetsId.Add(mediaId);
            }
            //Return list of created media asset IDs

            Console.WriteLine($"listingCase.MediaAssets count after upload: {listingCase.MediaAssets?.Count ?? 0}");
            return mediaAssetsId;

        }

        public async Task<(Stream Content, string ContentType, string FileName)> DownloadMediaAssetsAsync(int mediaAssetId) {

            //Validate mediaAsset exists
            var mediaAsset = await _mediaAssetRepository.GetMediaAssetByMediaId(mediaAssetId);
            if (mediaAsset == null || mediaAsset.IsDeleted == true)
            {
                throw new InvalidOperationException("No valid  mediaAsset found");

            }

            //Get Media Url
            var mediaUrl = mediaAsset.MediaUrl;

            //Download file from Azure Blob Storage using URL
            var downloadResult = await _azureBlobStorageService.DownloadAsync(mediaUrl);

            if (downloadResult.Content == null)
            {
                throw new InvalidOperationException("Download failed - no content received");
            }
            //return Content, ContentType, FileName
            return downloadResult;
        }

    }

}