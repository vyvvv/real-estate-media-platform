using RealEstateMediaPlatform.API.Models;

namespace RealEstateMediaPlatform.API.Repositories.MediaAssetRepositories
{
    public interface IMediaAssetRepository
    {
    Task<int> CreateMediaAssets(MediaAsset mediaAsset);
    Task<MediaAsset?> GetMediaAssetByMediaId(int mediaAssetId);
    Task<ListingCase?> GetValidListingCaseById(int listingCaseId);
    Task<ListingCase?> GetValidListingCaseWithMediaAsync(int listingCaseId);
    }
}
