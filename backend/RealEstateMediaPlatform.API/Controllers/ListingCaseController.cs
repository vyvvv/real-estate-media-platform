using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateMediaPlatform.API.Common;
using RealEstateMediaPlatform.API.DTOs.ListingCase;
using RealEstateMediaPlatform.API.DTOs.ListingCase.Agents;
using RealEstateMediaPlatform.API.DTOs.ListingCase.Medias;
using RealEstateMediaPlatform.API.Enums;
using RealEstateMediaPlatform.API.Services.ListingCaseServices;



namespace RealEstateMediaPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingCaseController : BaseController
    {
        private readonly IListingCaseService _listingCaseService;
        private readonly IValidator<ListingCaseCreateRequestDto> _listingCaseCreateValidator;

        public ListingCaseController(IListingCaseService listingCaseService, IValidator<ListingCaseCreateRequestDto> listingCaseCreateValidator) {

            _listingCaseService = listingCaseService;
            _listingCaseCreateValidator = listingCaseCreateValidator;

        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin,PhotographyCompany")]
        public async Task<IActionResult> CreateListingCase([FromBody] ListingCaseCreateRequestDto listingCaseCreateRequestDto)
        {
            
            var (userId,_) = GetCurrentUserInfo();

            //Use FluentValidation
            var ValidationResult = await _listingCaseCreateValidator.ValidateAsync(listingCaseCreateRequestDto);

            if (!ValidationResult.IsValid) { return BadRequest(ValidationResult.Errors); }

            var result = await _listingCaseService.CreateListingCaseAsync(listingCaseCreateRequestDto, userId);
            return Ok(ApiResponse<object>.Success(result, "Listing case created successfully"));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllListingCases([FromQuery] ListingQueryParameterDto queryParams)
        {
            
            var (userId, role) = GetCurrentUserInfo();

            //Use default If no params
            queryParams ??= new ListingQueryParameterDto();
            var result = await _listingCaseService.GetAllListingCasesAsync(userId, role, queryParams);
            return Ok(ApiResponse<object>.Success(result, "Listing cases retrieved successfully"));

        }

        [HttpPost("addagent")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAgentToListingCase([FromBody] AddAgentToListingCaseRequestDto addAgentRequestDto)
        {
            
            await _listingCaseService.AddAgentToListingCaseAsync(addAgentRequestDto);
            return Ok(ApiResponse<object>.Success("Added Agent successfully"));

        }

        [HttpPatch("{listingCaseId}")] //patch 适用于局部更新， put用于整体更新
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateListingCaseByAdmin(int listingCaseId, [FromBody] ListingCaseUpdateRequestDto listingCaseUpdateRequestDto)
        {
            var (userId, _) = GetCurrentUserInfo();
            var result = await _listingCaseService.UpdateListingCaseByAdminAsync(listingCaseId, listingCaseUpdateRequestDto,userId);
            return Ok(ApiResponse<object>.Success(result, "Updated successfully"));
        }

        [HttpDelete("{listingCaseId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteListingCase(int listingCaseId)
        {
            var (userId, _) = GetCurrentUserInfo();
            await _listingCaseService.DeleteListingCaseAsync(listingCaseId,userId);
            return Ok(ApiResponse<object>.Success("Deleted successfully"));
        }


        [HttpGet("{listingCaseId}")]
        [Authorize]
        public async Task<IActionResult> GetListingCaseDetailById(int listingCaseId)
        {
            
            var (userId, role) = GetCurrentUserInfo();
            var result = await _listingCaseService.GetListingCaseDetailByIdAsync(listingCaseId, userId, role);
            return Ok(ApiResponse<object>.Success(result, "Listing case details retrieved successfully"));

        }

        [HttpPost("{listingCaseId}/GenerateShareLink")]
        [Authorize]
        public async Task<IActionResult> GenerateShareLink(int listingCaseId)
        {
            var (userId, _) = GetCurrentUserInfo();
            var result = await _listingCaseService.GenerateShareLinkAsync(userId,listingCaseId);
            return Ok(ApiResponse<object>.Success($"ListingCase:{listingCaseId}\n Share Link:{result}"));
        }

        [HttpGet("{listingCaseId}/view/{token}")]
        [AllowAnonymous]
        public async Task<IActionResult> ViewShareLinkListingCase(int listingCaseId,string token)
        { 
            var listingCase = await _listingCaseService.ViewShareLinkAsync(listingCaseId,token);
            return Ok(ApiResponse<object>.Success(listingCase ,$"Listing case retrieved via share link"));
        }


        [HttpPatch("{listingCaseId}/status/{newStatus}")]
        [Authorize]
        public async Task<IActionResult> UpdateListingCaseStatus(int listingCaseId, ListCaseStatus newStatus) {

            var (userId, role) = GetCurrentUserInfo();
            var result = await _listingCaseService.UpdateListingCaseStatusAsync(listingCaseId, newStatus, userId,role);
            return Ok(ApiResponse<object>.Success(result, "Listing case status updated"));

        }

        [HttpGet("{listingCaseId}/media")]
        [Authorize]

        public async Task<IActionResult> GetListingCaseMediaAssets(int listingCaseId)
        {

            var (userId, role) = GetCurrentUserInfo();
            var result = await _listingCaseService.GetListingCaseMediaAssetAsync(listingCaseId, userId, role);
            return Ok(ApiResponse<object>.Success(result, "Listing case MediaAssets retrieved successfully"));

        }


        [HttpGet("{listingCaseId}/media/{mediaType}")]
        [Authorize]
        
        public async Task<IActionResult> GetMediaAssetsByType(int listingCaseId, MediaAssetType mediaType)
        {
            var(userId, role) = GetCurrentUserInfo();
            var result = await _listingCaseService.GetMediaAssetsByTypeAsync(listingCaseId,mediaType,userId, role);
            return Ok(ApiResponse<object>.Success(result, $"MediaAssets ({mediaType}) retrieved successfully"));
        }


        [HttpGet("{listingCaseId}/casecontact")]
        [Authorize]

        public async Task<IActionResult> GetListingCaseContact(int listingCaseId)
        {

            var (userId, role) = GetCurrentUserInfo();
            var result = await _listingCaseService.GetListingCaseContactAsync(listingCaseId, userId, role);
            return Ok(ApiResponse<object>.Success(result, "Listing case contacts retrieved successfully"));

        }


        [HttpGet("listings/{listingCaseId}/download")]
        [Authorize]
        public async Task<IActionResult> DownloadListingCaseMediaAsZip(int listingCaseId)
        {
            var (userId, role) = GetCurrentUserInfo();
            var zipStream = await _listingCaseService.DownloadListingCaseMediaAsZipAsync(listingCaseId, userId, role);
            return File(zipStream, "application/zip", $"ListingCase_{listingCaseId}_Media.zip");
        }

        [HttpPut("listings/{listingCaseId}/coverImage")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetCoverImage(int listingCaseId, [FromBody] SetCoverImageRequestDto setCoverImageRequestDto)
        {
            var (userId, _) = GetCurrentUserInfo();
            var result = await _listingCaseService.SetCoverImageAsync(userId,listingCaseId, setCoverImageRequestDto.ImageId);
            return Ok(ApiResponse<object>.Success(result, "ListingCase CoverImage updated successfully"));
        }

        [HttpPut("listings/{listingCaseId}/SelectDisplayMedia")]
        [Authorize(Roles = "Agent,Admin")]
        public async Task<IActionResult> SelectDisplayMedia(int listingCaseId, [FromBody]SelectedMediasRequestDto selectedMediasDto)
        {
            var (userId, _) = GetCurrentUserInfo();
            var selectedMedia = await _listingCaseService.SelectDisplayMediaAsync(userId, listingCaseId, selectedMediasDto.mediaIds);
            return Ok(ApiResponse<object>.Success(selectedMedia, "Display Media Selected successfully"));
        }

        [HttpGet("listings/{listingCaseId}/FinalMediaSelection")]
        [Authorize(Roles = "Agent,Admin")]
        public async Task<IActionResult> GetFinalMediaSelection(int listingCaseId)
        {
            var (userId, role) = GetCurrentUserInfo();
            var finalMediaSeletion = await _listingCaseService.GetFinalMediaSelectionAsync(userId,role,listingCaseId);
            return Ok(ApiResponse<object>.Success(finalMediaSeletion, " Final media selection retrieved successfully"));
        }


        [HttpDelete("media/{mediaId}")] // might move to independent controller
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMediaAsset(int mediaId)
        {
            var (userId, _) = GetCurrentUserInfo();
            await _listingCaseService.DeleteMediaAssetAsync(mediaId,userId);
            return Ok(ApiResponse<object>.Success("Media Deleted successfully"));
        }


        [HttpDelete("casecontact/{contactId}")] // might move to independent controller
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCaseContact(int contactId)
        {
            await _listingCaseService.DeleteCaseContactAsync(contactId);
            return Ok(ApiResponse<object>.Success("Case Contact Deleted successfully"));
        }

    }
}
