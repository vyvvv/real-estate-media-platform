using RealEstateMediaPlatform.API.DTOs.Admin;
using RealEstateMediaPlatform.API.DTOs.Agent;
using RealEstateMediaPlatform.API.DTOs.PhotographyCompany;
using RealEstateMediaPlatform.API.DTOs.User;
using RealEstateMediaPlatform.API.Models;
using System.ComponentModel.Design;
//I开头文件代表是interface接口文件，接口文件只定义方法，但不会有具体实现过程。
namespace RealEstateMediaPlatform.API.Services.UserServices
{
    public interface IUserService
    {
        Task<AdminRegisterResponseDto> CreateAdminAccountAsync(AdminRegisterRequestDto adminRegisterRequestDto, string role);
        Task<PhotographyCompanyRegisterResponseDto> CreatePhotographyCompanyAsync(PhotographyCompanyRegisterRequestDto photographyCompanyRegisterRequestDto, string role);
        Task<UserLoginResponseDto> LoginAsync(UserLoginRequestDto userLoginRequestDto);
        Task<List<PhotographyCompanyRegisterResponseDto>> GetAllPhotographyCompanyAsync();
        Task<AgentRegisterResponseDto> CreateAgentAsync(AgentRegisterRequestDto agentRegisterRequestDt, string role);
        Task<List<AgentRegisterResponseDto>> GetAllAgentAsync();
        Task<UserCurrentResponseDto> GetCurrentUserAsync(string userId,string role);
        Task<bool> UpdateAccountPasswordAsync(string userId, UserUpdatePasswordRequestDto updatePasswordDto);
        Task<AgentGetDetailResponseDto> SearchAgentByEmailAsync(string agentEmail);
        Task<bool> AddAgentToCompanyByEmailAsync(string companyId, string email);
        Task<List<AgentGetDetailResponseDto>> GetAgentsForCompanyAsync(string companyId);
    }
}
// 