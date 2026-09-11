using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealEstateMediaPlatform.API.Data;
using RealEstateMediaPlatform.API.DTOs.ListingCase;
using RealEstateMediaPlatform.API.Models;
using RealEstateMediaPlatform.API.QueryHelpers;


namespace RealEstateMediaPlatform.API.Repositories.ListingCaseRepositories
{
    public class ListingCaseRepository : IListingCaseRepository
    {
        private readonly RecamDbContext _dbContext;
        private readonly IMapper _mapper;

        public ListingCaseRepository(RecamDbContext dbContext, IMapper mapper)
        {

            _dbContext = dbContext;
            _mapper = mapper;
        }

        //Create listingCase for all user
        public async Task<ListingCase> CreateListingCaseAsync(ListingCaseCreateRequestDto listingCaseCreateRequestDto, string userId)
        {
            var listingCase = _mapper.Map<ListingCase>(listingCaseCreateRequestDto);
            listingCase.UserId = userId;
            var result = await _dbContext.ListingCases.AddAsync(listingCase);
            await _dbContext.SaveChangesAsync();
            return result.Entity;

        }

        //Update ListingCase
        public async Task<(ListingCase listingCase,string updateFiles)> UpdateListingCaseByAdminAsync(ListingCase existingCase, ListingCaseUpdateRequestDto listingCaseUpdateRequestDto, string userId)
        {
            _mapper.Map(listingCaseUpdateRequestDto, existingCase);
            existingCase.UpdatedAt = DateTime.Now;
            existingCase.UpdatedBy = userId;
            var UpdateFields = GetUpdateFields(existingCase);
            await _dbContext.SaveChangesAsync();
            return (existingCase,UpdateFields);
        }

        // Delete ListingCase and related data
        public async Task<bool> DeleteListingCaseAsync(ListingCase existingCase)
        {

            //Soft delete the main ListingCase
            existingCase.IsDeleted = true;

            //Soft delete related MediaAssets

            foreach (var mediaAsset in existingCase.MediaAssets)
            {
                mediaAsset.IsDeleted = true;
            }

            //Soft delete related caseContacts
            foreach (var caseContact in existingCase.CaseContacts)
            {
                caseContact.IsDeleted = true;
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }


        // Soft delete the media
        public async Task<bool> DeleteMediaAssetAsync(Models.MediaAsset mediaAsset)
        {
            mediaAsset.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }


        // Soft delete the case contact
        public async Task<bool> DeleteCaseContactAsync(Recam.Models.CaseContact casecontact)
        {
            casecontact.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        //Get Valid MediaAsset by checking Id
        public async Task<Recam.Models.MediaAsset?> GetValidMediaByIdAsync(int mediaId)
        {
            var existingMedia = await _dbContext.MediaAssets
                .FirstOrDefaultAsync(m=>m.Id== mediaId &&!m.IsDeleted);     

            if (existingMedia == null)
            {
                throw new KeyNotFoundException("no media found!");
            }
            return existingMedia; 
        }

        //get valid case contact

        public async Task<Recam.Models.CaseContact?> GetValidCaseContactByIdAsync(int contactId)
        {

            var exsitContact = await _dbContext.CaseContacts.FirstOrDefaultAsync(c => c.ContactId == contactId && !c.IsDeleted);
            if (exsitContact == null)
            {
                throw new KeyNotFoundException("no case contact found!");
            } 

            return exsitContact;

        }



        //Admin get listingCase
        public async Task<List<ListingCase>> GetAllListingCasesForAdminAsync(ListingQueryParameterDto queryParams)
        {
            var query = _dbContext.ListingCases.Where(lc => !lc.IsDeleted);
            //Apply filters, sorting, and pagination
            query = ListingCaseQueryHelper.ApplyFilters(query, queryParams);
            query = ListingCaseQueryHelper.ApplySorting(query, queryParams);
            query = ListingCaseQueryHelper.ApplyPaging(query, queryParams);

            return await query.ToListAsync();

        }

        //Agent get listingCase
        public async Task<List<ListingCase>> GetAllListingCasesForAgentAsync(string agentId, ListingQueryParameterDto queryParams)
        {
            var query = _dbContext.ListingCases
                .Where(lc => !lc.IsDeleted)
                .Where(lc => lc.Agents.Any(a => a.Id == agentId));

            query = ListingCaseQueryHelper.ApplyFilters(query, queryParams);
            query = ListingCaseQueryHelper.ApplySorting(query, queryParams);
            query = ListingCaseQueryHelper.ApplyPaging(query, queryParams);

            return await query.ToListAsync();

        }

        //Company get listingCase
        public async Task<List<ListingCase>> GetAllListingCasesForCompanyAsync(string companyId, ListingQueryParameterDto queryParams)
        {
            var query = _dbContext.ListingCases
                .Where(lc => !lc.IsDeleted)
                .Where(lc => lc.UserId == companyId);
            query = ListingCaseQueryHelper.ApplyFilters(query, queryParams);
            query = ListingCaseQueryHelper.ApplySorting(query, queryParams);
            query = ListingCaseQueryHelper.ApplyPaging(query, queryParams);

            return await query.ToListAsync();
        }


        // Get ListingCase by ID
        public async Task<ListingCase?> GetListingCaseByIdAsync(int listingCaseId)
        {
            return await _dbContext.ListingCases
                .Include(lc => lc.Agents)
                .Include(lc => lc.User)
                .FirstOrDefaultAsync(lc => lc.Id == listingCaseId && !lc.IsDeleted);
        }

        //Get ListingCase Detail by ID
        public async Task<ListingCase?> GetListingCaseDetailByIdAsync(int listingCaseId)
        {
            return await _dbContext.ListingCases
            .Include(lc => lc.Agents)
            .Include(lc => lc.CaseContacts.Where(cc => !cc.IsDeleted))
            .Include(lc => lc.MediaAssets.Where(ma => !ma.IsDeleted))
            .FirstOrDefaultAsync(lc => lc.Id == listingCaseId && !lc.IsDeleted);
        }


        // Get Agent by ID

        public async Task<Agent?> GetAgentByIdAsync(string agentId)
        {
            return await _dbContext.Agents.FirstOrDefaultAsync(a => a.Id == agentId);
        }


        // Assigns an agent to a listing case
        public async Task<bool> AddAgentToListingCaseAsync(ListingCase listingCase, Agent agent)
        {

            listingCase.Agents.Add(agent);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<ListingCase> UpdateListingCaseStatusAsync(ListingCase listingCase)
        {

            _dbContext.ListingCases.Update(listingCase);
            await _dbContext.SaveChangesAsync();
            return listingCase;

        }


        public async Task<MediaAsset> SetImageAsHeroAsync(ListingCase listingCase, MediaAsset image) {
 
            //Clear all mediaAssets' IsHero to false
            foreach (var media in listingCase.MediaAssets)
            {
                media.IsHero = false;
            }

            // Set selected image as Hero
            image.IsHero = true;

            await _dbContext.SaveChangesAsync();

            return image;

        }


        //Get listingCase updated details
        private string GetUpdateFields(object entity)
        {
            var updatedFields = new List<string>();
            var entry = _dbContext.Entry(entity);
                if (entry.State == EntityState.Modified)
                {
                    foreach (var property in entry.Properties)
                    {
                        if (property.IsModified)
                        {
                            var oldValue = property.OriginalValue?.ToString() ?? null;
                            var newValue = property.CurrentValue?.ToString() ?? null;
                        updatedFields.Add($"{property.Metadata.Name}:'{oldValue}'->'{newValue}'");
                        }

                    }
                }
  
            return string.Join(";", updatedFields);

        }


        //Get all media Ids for the listingCase
        public async Task<List<int>> GetValidMediaIdsAsync(int listingCaseId)
        {
            return await _dbContext.MediaAssets
                .Where(m =>m.ListingCaseId == listingCaseId && !m.IsDeleted)
                .Select(m =>m.Id)
                .ToListAsync();
        }

        //Update listingCase's media selection
        public async Task<bool> UpdateMediaSelectionAsync(int listingCaseId, List<int> mediaIds)
        {
            var ValildMedias =await  _dbContext.MediaAssets.Where(m => m.ListingCaseId == listingCaseId && !m.IsDeleted).ToListAsync();
            foreach(var media in ValildMedias)
            {
                media.IsSelect = mediaIds.Contains(media.Id);

            }
            await _dbContext.SaveChangesAsync();

            return true;
              
        }

        //Get listing case all selected Media Ids
        public async Task<List<int>> GetSelectedMediaIdsAsync(int listingCaseId) {
            return await _dbContext.MediaAssets
                .Where(m => m.ListingCaseId == listingCaseId && !m.IsDeleted && m.IsSelect)
                .Select(m=>m.Id)
                .ToListAsync();

        }


        public async Task<string> GenerateShareLinkAsync(ListingCase listingCase)
        {
            var token = Guid.NewGuid().ToString("N");
            var url = $"listingcase/{listingCase.Id}/view/{token}";
            listingCase.ShareLinkUrl = url;
            listingCase.ShareLinkToken = token;
            listingCase.ShareLinkGeneratedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return url;
        }

    }
}

