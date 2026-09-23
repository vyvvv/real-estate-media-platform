using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstateMediaPlatform.API.Data;
using RealEstateMediaPlatform.API.Models;
using System.Data;

namespace RealEstateMediaPlatform.API.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RealEstateDbContext _dbContext;
        private readonly UserManager<User> _userManager;


        public UserRepository(RealEstateDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        //Create user in database
        public async Task<User> CreateAsync(User user, string password)
        {

            var result = await _userManager.CreateAsync(user, password);

            // If user creation failed and throw exception
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to create user: {errors}");
            }

            return user;

        }

        //Add role to user

        public async Task AddToRoleAsync(User user, string role)
        {

            var result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to add user role: {errors}");
            }

        }


        //Find user by email

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);

        }

        //Verify user password
        public async Task<bool> CheckPasswordAsync(User user, string loginPassoword)
        {
            return await _userManager.CheckPasswordAsync(user, loginPassoword);

        }


        public async Task<bool> UpdateAccountPasswordAsync(User currentUser, string currentPassword, string newPassword)
        {

            var result = await _userManager.ChangePasswordAsync(currentUser, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Password update failed: {errors}");
            }
            return true;
        }



        //Get user roles

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            var result = await _userManager.GetRolesAsync(user);
            return result;
        }


        //Get users by role
        public async Task<List<User>> GetUsersByRoleAsync(string role)
        {
            return (await _userManager.GetUsersInRoleAsync(role)).ToList();
        }


        //Get user by ID
        public async Task<User?> GetUserByIdAsync(string userId)
        {
            return await _dbContext.Users
            .Include(u => u.ListingCase.Where(lc => !lc.IsDeleted))
            .FirstOrDefaultAsync(u => u.Id == userId);
        }

        //Get PhotographyCompany by Id
        public async Task<PhotographyCompany?> GetPhotographyCompanyByIdAsync(string userId)
        {
            return await _dbContext.Set<PhotographyCompany>()
                .Include(pc => pc.ListingCase.Where(lc => !lc.IsDeleted))
                .Include(pc => pc.Agents)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        //Get all photography companies
        public async Task<List<PhotographyCompany>> GetAllPhotographyCompanyAsync()
        {
            // Get PhotographyCompanys table
            return await _dbContext.PhotographyCompanies.ToListAsync();
        }

        //Get all agents
        public async Task<List<Agent>> GetAllAgentAsync()
        {
            return await _dbContext.Agents.ToListAsync();

        }



        //Get Admin ListingCases Id 

        public async Task<List<int>> GetAdminListingCasesId()
        {
            return await _dbContext.ListingCases
                                 .Where(lc => !lc.IsDeleted)
                                 .Select(lc => lc.Id)
                                 .ToListAsync();
        }

        //Get Agent ListingCases Id
        public async Task<List<int>> GetAgentListingCasesId(string userId)
        {
            return await _dbContext.ListingCases
                        .Where(lc => lc.Agents.Any(a => a.Id == userId) && !lc.IsDeleted)
                        .Select(lc => lc.Id)
                        .ToListAsync();
        }

        //Get PhotoCompany ListingCases Id
        public async Task<List<int>> GetCompanyListingCasesId(string userId)
        {
            return await _dbContext.ListingCases
                        .Where(lc => lc.UserId == userId && !lc.IsDeleted)
                        .Select(lc => lc.Id)
                        .ToListAsync();
        }

        //Get agent by its email

        public async Task<Agent?> GetAgentByEmailAsync(string email)
        {
            return await _dbContext.Agents.FirstOrDefaultAsync(a => a.Email == email);
        }

        //Add Agent to photocompany
        public async Task<bool> AddAgentToPhotoCompanyAsync(PhotographyCompany photoCompany, Agent targetAgent)
        {
    
            photoCompany.Agents.Add(targetAgent);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public List<Agent> GetAgentsForCompanyAsync(PhotographyCompany photoCompany)
        {

            return photoCompany.Agents.ToList();

        }
    }
    
}


    

