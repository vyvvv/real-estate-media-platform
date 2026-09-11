using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using RealEstateMediaPlatform.API.Enums;

namespace RealEstateMediaPlatform.API.Services.MediaAssetServices
{
    public interface IMediaAssetService
    {

        Task<List<int>>UploadMediaAssetsAsync(List<IFormFile> files, MediaAssetType type, int listingCaseId,string userId);

        Task<(Stream Content, string ContentType, string FileName)> DownloadMediaAssetsAsync(int mediaAssetId);
    }


}