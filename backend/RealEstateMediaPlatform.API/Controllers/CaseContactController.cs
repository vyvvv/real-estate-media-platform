using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RealEstateMediaPlatform.API.Data;


namespace RealEstateMediaPlatform.API
{
    [Route("api/[controller]")]
    [ApiController]
   public class CaseContactController : BaseController
    {
        private readonly ICaseContactService _caseContactService;
        public CaseContactController(ICaseContactService caseContactService)
        {
            _caseContactService = caseContactService;

        }

        [HttpPost("casecontact")]
        [Authorize(Roles = "Admin,Agent")]

        public async Task<IActionResult> CreateCaseContact([FromBody] CaseContactCreateRequestDto caseContactCreateRequestDto)
        {
            var result = await _caseContactService.CreateCaseContactAsync(caseContactCreateRequestDto);
            return Ok(ApiResponse<object>.Success(result, "Case contact created!"));

        }


    }
}
