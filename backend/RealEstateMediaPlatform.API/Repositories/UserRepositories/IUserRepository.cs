using RealEstateMediaPlatform.API.Models;


namespace RealEstateMediaPlatform.API.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<List<PhotographyCompany>> GetAllPhotographyCompanyAsync();
        Task<List<Agent>> GetAllAgentAsync();
        Task<User?> FindByEmailAsync(string email);
        Task<User> CreateAsync(User user, string password);
        Task AddToRoleAsync(User user, string role);
        Task<bool> CheckPasswordAsync(User user, string loginPassoword);
        Task<bool> UpdateAccountPasswordAsync(User currentUser, string currentPassword,string newPassword);

        Task<IList<string>> GetRolesAsync(User user);
        Task<List<User>> GetUsersByRoleAsync(string role);
        Task<User?> GetUserByIdAsync(string userId);
        Task<List<int>> GetAdminListingCasesId();
        Task<List<int>> GetAgentListingCasesId(string userId);
        Task<List<int>> GetCompanyListingCasesId(string userId);
        Task<Agent?> GetAgentByEmailAsync(string email);
        Task<bool> AddAgentToPhotoCompanyAsync(PhotographyCompany photoCompany, Agent targetAgent);
        Task<PhotographyCompany> GetPhotographyCompanyByIdAsync(string companyId);
        List<Agent> GetAgentsForCompanyAsync(PhotographyCompany photoCompany);
    }
}
