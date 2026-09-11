using Microsoft.AspNetCore.Mvc;
using RealEstateMediaPlatform.API.Common;
using RealEstateMediaPlatform.API.Enums;
using RealEstateMediaPlatform.API.Services.MediaAssetServices;


namespace RealEstateMediaPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaAssetController : BaseController
    {
        private readonly IMediaAssetService _mediaAssetService;
        public MediaAssetController(IMediaAssetService mediaAssetService)
        {
            _mediaAssetService = mediaAssetService;
        }

        [HttpPost("upload")]
        //[Authorize(Roles = "Admin,PhotographyCompany")]
        public async Task<IActionResult> UploadMediaAssets([FromForm] List<IFormFile> files, [FromForm] MediaAssetType type, [FromForm] int listingCaseId)
        {
            var (userId, _) = GetCurrentUserInfo();
            var result = await _mediaAssetService.UploadMediaAssetsAsync(files, type, listingCaseId, userId);
            return Ok(ApiResponse<object>.Success(result, "Media File Uploaded"));

        }

        [HttpGet("download/{mediaAssetId}")]
        public async Task<IActionResult> DownloadMediaAssets(int mediaAssetId)
        {
            var (fileStream, contentType, fileName) = await _mediaAssetService.DownloadMediaAssetsAsync(mediaAssetId);
            return File(fileStream, contentType, fileName);

        }

    }

}
