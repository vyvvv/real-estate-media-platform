using RealEstateMediaPlatform.API.Exceptions;
using RealEstateMediaPlatform.API.Models;

namespace RealEstateMediaPlatform.API.Services.AzureBlobStorage
{
    // Keeps ordinary listing operations available without an Azure connection.
    public class DisabledBlobStorageService : IAzureBlobStorageService
    {
        private const string DisabledMessage = "媒体存储暂未启用，暂不支持上传、下载或打包文件。";

        public Task<string> UploadAsync(IFormFile file, string folder)
            => throw new FeatureNotAvailableException(DisabledMessage);

        public Task<(Stream Content, string ContentType, string FileName)> DownloadAsync(string blobUrl)
            => throw new FeatureNotAvailableException(DisabledMessage);

        public Task<MemoryStream?> ZipMediaAssetsAsync(List<MediaAsset> mediaAssets)
            => throw new FeatureNotAvailableException(DisabledMessage);
    }
}
