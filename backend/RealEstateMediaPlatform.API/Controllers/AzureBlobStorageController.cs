using Microsoft.AspNetCore.Mvc;
using RealEstateMediaPlatform.API.Common;
using RealEstateMediaPlatform.API.Services.AzureBlobStorage;
using Swashbuckle.AspNetCore.Annotations;
using RealEstateMediaPlatform.API.DTOs;

namespace RealEstateMediaPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureBlobStorageController : ControllerBase
    {
        private readonly IAzureBlobStorageService _azureBlobStorageService;

        public AzureBlobStorageController(IAzureBlobStorageService azureBlobStorageService) {

            _azureBlobStorageService = azureBlobStorageService;

        }

        [HttpPost("uploads")]
        public async Task<IActionResult> Upload([FromForm] FileUploadDto request)
        {
            var upload = await _azureBlobStorageService.UploadAsync(request.File, "MediaAsset");
            return Ok(ApiResponse<object>.Success(upload, "Upload successful"));//return type to be confirm

        }


    }
}
