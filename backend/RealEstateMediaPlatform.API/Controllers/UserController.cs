using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateMediaPlatform.API.Common;
using RealEstateMediaPlatform.API.DTOs.Admin;
using RealEstateMediaPlatform.API.DTOs.Agent;
using RealEstateMediaPlatform.API.DTOs.PhotographyCompany;
using RealEstateMediaPlatform.API.DTOs.User;
using RealEstateMediaPlatform.API.Services.UserServices;


namespace RealEstateMediaPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        //这里是依赖注入。readonly代表着只能在构建或者创造函数的时候赋值一次，之后就不能更改了

        public UsersController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequestDto userLoginRequestDto)
        {
            var result = await _userService.LoginAsync(userLoginRequestDto);
            return Ok(ApiResponse<object>.Success(result, "Login successful"));
        }


        [HttpPost("CreateAdmin")]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminRegisterRequestDto adminRegisterRequestDto)
        {
            var result = await _userService.CreateAdminAccountAsync(adminRegisterRequestDto, "Admin");
            return Ok(ApiResponse<object>.Success(result, "Admin user created successfully"));
        }


        [HttpPost("CreateAgent")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAgent([FromBody] AgentRegisterRequestDto agentRegisterRequestDto)
        {

            var result = await _userService.CreateAgentAsync(agentRegisterRequestDto, "Agent");
            return Ok(ApiResponse<object>.Success(result, "Agent created successfully"));
        }


        [HttpPost("CreatePhotographyCompany")]
        public async Task<IActionResult> CreatePhotographyCompany([FromBody] PhotographyCompanyRegisterRequestDto photographyCompanyRegisterRequestDto)
        {
            var result = await _userService.CreatePhotographyCompanyAsync(photographyCompanyRegisterRequestDto, "PhotographyCompany");
            return Ok(ApiResponse<object>.Success(result, "PhotographyCompany user created successfully"));
        }


        [HttpGet("GetAllPhotographyCompany")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPhotographyCompany()
        {
            var result = await _userService.GetAllPhotographyCompanyAsync();
            return Ok(ApiResponse<object>.Success(result, "All photography companies retrieved successfully"));
        }


        [HttpGet("GetAllAgent")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAgent()
        {
            var result = await _userService.GetAllAgentAsync();
            return Ok(ApiResponse<object>.Success(result, "All Agent retrieved successfully"));
        }

        [HttpGet("Me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {

            var (userId, role) = GetCurrentUserInfo();
            var result = await _userService.GetCurrentUserAsync(userId, role);
            return Ok(ApiResponse<object>.Success(result, "Current user information retrieved successfully"));

        }

        [HttpPatch("password")]
        [Authorize]
        public async Task<IActionResult> UpdateAccountPassword([FromBody] UserUpdatePasswordRequestDto updatePasswordDto)
        {
            var (userId, _) = GetCurrentUserInfo();
            var result = await _userService.UpdateAccountPasswordAsync(userId, updatePasswordDto);
            return Ok(ApiResponse<object>.Success(result, "Password updated successfully"));

        }

        [HttpGet("SearchAgent/{agentEmail}")]
        [Authorize(Roles = "Admin,PhotographyCompany")]
        public async Task<IActionResult> SearchAgentByEmail(string agentEmail)
        {
            var result= await _userService.SearchAgentByEmailAsync(agentEmail);
            return Ok(ApiResponse<object>.Success(result, "Agent found successfully"));
        }


        [HttpPost("AddAgentByEmail/{agentEmail}")]
        [Authorize(Roles = "PhotographyCompany")]
        //后续可以考虑通过FromBody 传递参数，目前暂时不改
        public async Task<IActionResult> AddAgentToCompanyByEmail(string agentEmail)
        {
            var (companyId, _) = GetCurrentUserInfo();
            await _userService.AddAgentToCompanyByEmailAsync(companyId,agentEmail);
            return Ok(ApiResponse<object>.Success("Photography Company Added Agent successfully"));
        }

        [HttpGet("photographyCompanyAgents")]
        [Authorize(Roles= "PhotographyCompany")]
        public async Task<IActionResult> GetAgentsForCompany()
        {
            var (companyId, _) = GetCurrentUserInfo();
            var agents = await _userService.GetAgentsForCompanyAsync(companyId);
            return Ok(ApiResponse<object>.Success(agents,"Photography Company get agents successfully"));

        }

    }
}
