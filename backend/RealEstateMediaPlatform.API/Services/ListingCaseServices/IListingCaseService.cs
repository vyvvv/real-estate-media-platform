using RealEstateMediaPlatform.API.DTOs.CaseContact;
using RealEstateMediaPlatform.API.DTOs.ListingCase;
using RealEstateMediaPlatform.API.DTOs.ListingCase.Agents;
using RealEstateMediaPlatform.API.DTOs.ListingCase.Medias;
using RealEstateMediaPlatform.API.DTOs.MediaAsset;
using RealEstateMediaPlatform.API.Enums;


namespace RealEstateMediaPlatform.API.Services.ListingCaseServices
{
    public interface IListingCaseService
    {
        Task<ListingCaseCreateResponseDto> CreateListingCaseAsync(ListingCaseCreateRequestDto listingCaseCreateRequestDto, string userId);
        Task<List<ListingCaseGetResponseDto>> GetAllListingCasesAsync(string userId, string role, ListingQueryParameterDto queryParams);
        Task<bool> AddAgentToListingCaseAsync(AddAgentToListingCaseRequestDto addAgentRequestDto);
        Task<ListingCaseGetResponseDto> UpdateListingCaseByAdminAsync(int listingCaseId, ListingCaseUpdateRequestDto listingCaseUpdateRequestDto,string userId);
        Task<bool> DeleteListingCaseAsync(int listingCaseId,string userId);
        Task<ListingCaseGetDetailResponseDto> GetListingCaseDetailByIdAsync(int listingCaseId, string userId, string role);
        Task<ListingCaseGetResponseDto> UpdateListingCaseStatusAsync(int listingCaseId, ListCaseStatus newStatus, string userId, string role);
        Task<MediaAssetGroupedResponseDto> GetListingCaseMediaAssetAsync(int listingCaseId, string userId, string role);
        Task<List<CaseContactGetDetailResponseDto>> GetListingCaseContactAsync(int listingCaseId, string userId, string role);
        Task<bool> DeleteCaseContactAsync(int contactId);
        Task<bool> DeleteMediaAssetAsync(int mediaId,string userId);
        Task<MemoryStream> DownloadListingCaseMediaAsZipAsync(int listingCaseId,string userId, string role);
        Task<SetCoverImageResponseDto> SetCoverImageAsync(string userId, int listingCaseId, int imageId);
        Task<List<int>> SelectDisplayMediaAsync(string userId, int listingCaseId, List<int> mediaIds);
        Task<List<int>> GetFinalMediaSelectionAsync(string userId,string role, int listingCaseId);
        Task<string> GenerateShareLinkAsync(string userId,int listingCaseId);
        Task<ListingCaseGetResponseDto> ViewShareLinkAsync(int listingCaseId, string token);
        Task<List<MediaAssetGetDetailResponseDto>> GetMediaAssetsByTypeAsync(int listingCaseId, MediaAssetType mediaType, string userId, string role);

    }
}