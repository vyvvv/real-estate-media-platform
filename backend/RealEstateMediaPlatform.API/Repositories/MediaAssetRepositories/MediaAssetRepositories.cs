using Microsoft.EntityFrameworkCore;
using RealEstateMediaPlatform.API.Data;
using RealEstateMediaPlatform.API.Models;


namespace RealEstateMediaPlatform.API.Repositories.MediaAssetRepositories
{
    public class MediaAssetRepository : IMediaAssetRepository
    {

        private readonly RecamDbContext _dbContext;
        public MediaAssetRepository(RecamDbContext dbContext)
                {
                    _dbContext = dbContext;
            
                }

        // Get valid listingCase
        public async Task<ListingCase?> GetValidListingCaseById(int listingCaseId)
        { 
            return await _dbContext.ListingCases.FirstOrDefaultAsync(lc => lc.Id == listingCaseId && !lc.IsDeleted);   
        }

        //Get valid listingCase with MediaAssets
        public async Task<ListingCase?> GetValidListingCaseWithMediaAsync(int listingCaseId)
        {
            return await _dbContext.ListingCases
                .Include(lc => lc.MediaAssets.Where(m => !m.IsDeleted))
                .FirstOrDefaultAsync(lc => lc.Id == listingCaseId && !lc.IsDeleted);
        }

        public async Task<int> CreateMediaAssets(MediaAsset mediaAsset)
        { 
            // Add MediaAsset to database
            _dbContext.MediaAssets.Add(mediaAsset);
            await _dbContext.SaveChangesAsync();
            //Console.WriteLine($"Created mediaAsset,the ID is: {mediaAsset.Id}");
            return mediaAsset.Id;
        }

        //Find MediaAsset by media Id
       public async Task<MediaAsset?> GetMediaAssetByMediaId(int mediaAssetId)
        {
            return await _dbContext.MediaAssets.FindAsync(mediaAssetId);

        }

    }
}
