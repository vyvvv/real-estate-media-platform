using RealEstateMediaPlatform.API.DTOs.ListingCase;
using RealEstateMediaPlatform.API.Models;

namespace RealEstateMediaPlatform.API.Repositories.ListingCaseRepositories
{
    public interface IListingCaseRepository
    {
        Task<ListingCase> CreateListingCaseAsync(ListingCaseCreateRequestDto listingCaseCreateRequestDto, string userId);
        Task<List<ListingCase>> GetAllListingCasesForAdminAsync(ListingQueryParameterDto queryParams);
        Task<List<ListingCase>> GetAllListingCasesForAgentAsync(string userId, ListingQueryParameterDto queryParams);
        Task<List<ListingCase>> GetAllListingCasesForCompanyAsync(string userId, ListingQueryParameterDto queryParams);
        Task<bool> AddAgentToListingCaseAsync( ListingCase listingCase,Agent agent);   
        Task<ListingCase?> GetListingCaseByIdAsync(int listingCaseId);
        Task<ListingCase?> GetListingCaseDetailByIdAsync(int listingCaseId);

        Task<Agent?> GetAgentByIdAsync(string agentId);
        Task<(ListingCase listingCase, string updateFiles)> UpdateListingCaseByAdminAsync(ListingCase existingCase, ListingCaseUpdateRequestDto listingCaseUpdateRequestDto,string userId);
        Task<bool> DeleteListingCaseAsync(ListingCase existingCase);
        Task<ListingCase> UpdateListingCaseStatusAsync(ListingCase listingCase);

        Task<bool> DeleteMediaAssetAsync(Models.MediaAsset MediaAsset);
        Task<bool> DeleteCaseContactAsync(RealEstateMediaPlatform.API.Models.CaseContact casecontact);


        Task<Models.MediaAsset?> GetValidMediaByIdAsync(int mediaId);
        Task<RealEstateMediaPlatform.API.Models.CaseContact?> GetValidCaseContactByIdAsync(int contactId);
        Task<MediaAsset> SetImageAsHeroAsync(ListingCase listingCase, MediaAsset image);

        Task<List<int>> GetValidMediaIdsAsync(int listingCaseId);

        Task<bool> UpdateMediaSelectionAsync(int listingCaseId, List<int> mediaIds);
        Task<List<int>> GetSelectedMediaIdsAsync(int listingCaseId);
        Task<string> GenerateShareLinkAsync(ListingCase listingCase);
    }
}
