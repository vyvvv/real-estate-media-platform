using AutoMapper;
using RealEstateMediaPlatform.API.Collections;
using RealEstateMediaPlatform.API.Data;
using RealEstateMediaPlatform.API.DTOs.CaseContact;
using RealEstateMediaPlatform.API.DTOs.ListingCase;
using RealEstateMediaPlatform.API.DTOs.ListingCase.Agents;
using RealEstateMediaPlatform.API.DTOs.ListingCase.Medias;
using RealEstateMediaPlatform.API.DTOs.MediaAsset;
using RealEstateMediaPlatform.API.Enums;
using RealEstateMediaPlatform.API.Models;
using RealEstateMediaPlatform.API.Repositories.ListingCaseRepositories;
using RealEstateMediaPlatform.API.Services.AzureBlobStorage;


namespace RealEstateMediaPlatform.API.Services.ListingCaseServices
{
    public class ListingCaseService : IListingCaseService
    {
        private readonly IListingCaseRepository _listingCaseRepository;
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly MongoDbContext _mongoDbContext;
        private readonly IMapper _mapper;

        public ListingCaseService(IListingCaseRepository listingCaseRepository, IAzureBlobStorageService azureBlobStorageService, IMapper mapper, MongoDbContext mongoDbContext)
        {
            _listingCaseRepository = listingCaseRepository;
            _azureBlobStorageService = azureBlobStorageService;
            _mongoDbContext = mongoDbContext;
            _mapper = mapper;
        }

        //Create ListingCase
        public async Task<ListingCaseCreateResponseDto> CreateListingCaseAsync(ListingCaseCreateRequestDto listingCaseCreateRequestDto, string userId)
        {
            var listingCase = await _listingCaseRepository.CreateListingCaseAsync(listingCaseCreateRequestDto, userId);
            await SaveListingCaseEventAsync(
                  EventTypes.CASE_CREATE,
                  "Listing Case Created successfully",
                  listingCase.Id.ToString(),
                  listingCase.UserId,
                  true
                );

            return _mapper.Map<ListingCaseCreateResponseDto>(listingCase);
        }


        //Update ListingCase by Id
        public async Task<ListingCaseGetResponseDto> UpdateListingCaseByAdminAsync(int listingCaseId, ListingCaseUpdateRequestDto listingCaseUpdateRequestDto, string userId){
            //Get valid ListingCase
            var existingCase = await GetListingCaseWithValidationAsync(listingCaseId);
            var (updatedListingCase,updateFiles) = await _listingCaseRepository.UpdateListingCaseByAdminAsync(existingCase, listingCaseUpdateRequestDto,userId);
            
            await SaveListingCaseEventAsync(
                EventTypes.CASE_UPDATE,
                $"ListingCase updated by user {updatedListingCase.UpdatedBy}\n Updated:{updateFiles}",
                updatedListingCase.Id.ToString(),
                null,
                true 
                );

            return _mapper.Map<ListingCaseGetResponseDto>(updatedListingCase);
        }


        //Delete ListingCase by Id
        public async Task<bool> DeleteListingCaseAsync(int listingCaseId, string userId)
        {
            //Get valid ListingCase
            var existingCase = await GetListingCaseWithValidationAsync(listingCaseId);
            if (existingCase.ListcaseStatus == ListcaseStatus.Pending)//need to confirm the logical here futher
            { 
               throw new InvalidOperationException($"ListingCase in " +
                   $"{existingCase.ListcaseStatus} status not allow delete");
            }

            var result = await _listingCaseRepository.DeleteListingCaseAsync(existingCase);

            await SaveListingCaseEventAsync(
                  EventTypes.CASE_DELETE,
                  "Listing Case Deleted successfully",
                  listingCaseId.ToString(),
                  userId,
                  true
                );

            return result;
            }

        //Soft delete media by Id
        public async Task<bool> DeleteMediaAssetAsync(int mediaId, string userId)
        {
            var exsitMedia = await _listingCaseRepository.GetValidMediaByIdAsync(mediaId);
            await _listingCaseRepository.DeleteMediaAssetAsync(exsitMedia!);

            await SaveListingCaseEventAsync(
                       EventTypes.CASE_DELETE,
                       $"Media with Id '{mediaId}' is deleted successfully! ",
                       mediaId.ToString(),
                       userId,
                       true
                       );
            return true;
        }


        //Soft delete case contact by Id
        public async Task<bool> DeleteCaseContactAsync(int contactId)
        {
            var exsitContact = await _listingCaseRepository.GetValidCaseContactByIdAsync(contactId);
            await _listingCaseRepository.DeleteCaseContactAsync(exsitContact!);
            await SaveListingCaseEventAsync(
                       EventTypes.CASE_DELETE,
                       $"contact with Id '{contactId}' is deleted successfully! ",
                       contactId.ToString(),
                       null,
                       true
                       );
            return true;
        }


        //Get ListingCase by user role

        public async Task<List<ListingCaseGetResponseDto>> GetAllListingCasesAsync(string userId, string role, ListingQueryParameterDto queryParams)
        {
            List<ListingCase> listingCases;

            switch (role.ToLower())
            {
                case "admin":
                    listingCases = await _listingCaseRepository.GetAllListingCasesForAdminAsync(queryParams);
                    break;
                case "agent":
                    listingCases = await _listingCaseRepository.GetAllListingCasesForAgentAsync(userId, queryParams);
                    break;
                case "photographycompany":
                    listingCases = await _listingCaseRepository.GetAllListingCasesForCompanyAsync(userId, queryParams);
                    break;
                default:
                    throw new KeyNotFoundException($"Invalid role: {role}");
            }

            return _mapper.Map<List<ListingCaseGetResponseDto>>(listingCases);

        }


        //Get ListingCase Details by Id
        public async Task<ListingCaseGetDetailResponseDto> GetListingCaseDetailByIdAsync(int listingCaseId, string userId, string role)
        {
            //Get valid ListingCase details (includes Agents, CaseContacts, MediaAssets)
            var listingCase = await GetListingCaseDetailWithValidationAsync(listingCaseId);

            //Validate ListingCase Access by check role
            ValidateListingCaseAccess(listingCase, userId, role, "access");
            return _mapper.Map<ListingCaseGetDetailResponseDto>(listingCase);

        }


        // Assigns an agent to a listing case with business validation
        public async Task<bool> AddAgentToListingCaseAsync(AddAgentToListingCaseRequestDto addAgentRequestDto)
        {
            //Get valid ListingCase
            var listingCase = await GetListingCaseWithValidationAsync(addAgentRequestDto.ListingCaseId);

            var agent = await _listingCaseRepository
                .GetAgentByIdAsync(addAgentRequestDto.AgentId) ?? 
                throw new KeyNotFoundException
                ($"Agent with ID {addAgentRequestDto.AgentId} not found");

            if (listingCase.Agents.Any(a => a.Id == addAgentRequestDto.AgentId))
            {
                throw new InvalidOperationException
                ($"Agent {addAgentRequestDto.AgentId} already assigned to this listing case" +
                    $" {addAgentRequestDto.ListingCaseId}");
            }

            // Call Repository to execute the assignment operation
            return await _listingCaseRepository.AddAgentToListingCaseAsync(
                listingCase,agent);
        }


        //Update ListingCase Status
        public async Task<ListingCaseGetResponseDto> UpdateListingCaseStatusAsync(int listingCaseId, ListcaseStatus newStatus, string userId, string role)
        {
            var listingCase = await GetListingCaseWithValidationAsync(listingCaseId);
            //Validate ListingCase Access by check role
            ValidateListingCaseAccess(listingCase, userId, role, "access");

            //Validate status transition
            ValidateStatusTransition(listingCase.ListcaseStatus, newStatus);

            var previousStatus = listingCase.ListcaseStatus;
            //Update ListingCase
            listingCase.ListcaseStatus = newStatus;
            listingCase.UpdatedAt = DateTime.UtcNow;//mock new listingcase have to have this filed?
            listingCase.UpdatedBy = userId;//mock new listingcase have to have this filed?

            await _listingCaseRepository.UpdateListingCaseStatusAsync(listingCase);

            await SaveListingCaseEventAsync(
               EventTypes.CASE_UPDATE,
               $"ListingCase status updated from {previousStatus} to {listingCase.ListcaseStatus}",
               listingCase.Id.ToString(),
               userId,
               true
               );

            return _mapper.Map<ListingCaseGetResponseDto>(listingCase);


        }


        // Get MediaAssets by ListingCase Id, grouped by MediaType
        public async Task<MediaAssetGroupedResponseDto> GetListingCaseMediaAssetAsync(int listingCaseId, string userId, string role)
        {

            //Get valid ListingCase details (includes MediaAssets)
            var listingCase = await GetListingCaseDetailWithValidationAsync(listingCaseId);

            //Validate ListingCase Access by check role
            ValidateListingCaseAccess(listingCase, userId, role, "access");

            var groupedMedia = listingCase.MediaAssets
                .GroupBy(ma => ma.MediaType)
                .ToDictionary(g => g.Key, g => _mapper.Map<List<MediaAssetGetDetailResponseDto>>(g.ToList()));


            return new MediaAssetGroupedResponseDto
            {
                MediaAssetsByType = groupedMedia,
                TotalCount = listingCase.MediaAssets.Count,
            };
        }

        public async Task<List<MediaAssetGetDetailResponseDto>> GetMediaAssetsByTypeAsync(int listingCaseId, MediaAssetType mediaType,
            string userId, string role)
        {
            //Get valid ListingCase details (includes MediaAssets)
            var listingCase = await GetListingCaseDetailWithValidationAsync(listingCaseId);

            //Validate ListingCase Access by check role
            ValidateListingCaseAccess(listingCase, userId, role, "access");

            //Filter ONLY the media of the selected type
            var typeMedia = listingCase.MediaAssets.Where(ma=>ma.MediaType == mediaType).ToList();
            return _mapper.Map<List<MediaAssetGetDetailResponseDto>>(typeMedia);  
        }



        // Get CaseContacts by ListingCase Id
        public async Task<List<CaseContactGetDetailResponseDto>> GetListingCaseContactAsync(int listingCaseId, string userId, string role)
        {

            var listingCase = await GetListingCaseDetailWithValidationAsync(listingCaseId);
            //Validate ListingCase Access by check role
            ValidateListingCaseAccess(listingCase, userId, role, "access");
            var result = listingCase.CaseContacts.ToList();
            return _mapper.Map<List<CaseContactGetDetailResponseDto>>(result);

        }


        public async Task<SetCoverImageResponseDto> SetCoverImageAsync(string userId, int listingCaseId, int imageId)
        {
            var listingCase = await GetListingCaseDetailWithValidationAsync(listingCaseId);

            // this one move to repository as get media from media id???
            var image = listingCase.MediaAssets.FirstOrDefault(m => m.Id == imageId && !m.IsDeleted);
            if (image == null)
            {
                throw new KeyNotFoundException("Image not found in this listingCase");
            }

            if (image.IsHero)
            {
                throw new InvalidOperationException("Image is already set as cover image");
            }
            var  coverImage= await _listingCaseRepository.SetImageAsHeroAsync(listingCase, image);

            await SaveListingCaseEventAsync(
               EventTypes.CASE_MEDIA_UPDATE,
               $"Image with Id:{image.Id},\n Url:{image.MediaUrl}\n Set as CoverImage",
               listingCase.Id.ToString(),
               userId,
               true
               );

            return _mapper.Map<SetCoverImageResponseDto>(coverImage);

        }


        //Select media for listingCase
        public async Task<List<int>> SelectDisplayMediaAsync(string userId, int listingCaseId, List<int> mediaIds)
        {
            //No more than 10 media can be selected
            if (mediaIds.Count > 10)
            {
                throw new InvalidOperationException("No more than 10 media");
            }

            //Get all valid media from this listingCase
            var validMediaIds = await _listingCaseRepository.GetValidMediaIdsAsync(listingCaseId);
            
            if (validMediaIds.Count == 0)
            {
                throw new KeyNotFoundException($"no valid media found in listingCase");
            }

           
            var InValidMediaIds = mediaIds.Except(validMediaIds).ToList();
            if(InValidMediaIds.Any())
            {
                throw new InvalidOperationException
                    ($"Invalid selected media IDs: {string.Join(", ", InValidMediaIds)}");
            }

            await _listingCaseRepository.UpdateMediaSelectionAsync(listingCaseId, mediaIds);

            //Select Medias as requested
            var selectedMediaIds = await _listingCaseRepository.GetSelectedMediaIdsAsync(listingCaseId);

            //Save media selection event to database
            await SaveListingCaseEventAsync(
                     EventTypes.CASE_MEDIA_UPDATE,
                     $"Display media selected:[{string.Join(",",mediaIds)}] for listingCase {listingCaseId}",
                     listingCaseId.ToString(),
                     userId,
                     true
                     );

            return selectedMediaIds;
        }

        //Get listing case final media selection
        public async Task<List<int>>GetFinalMediaSelectionAsync(string userId, string role, int listingCaseId)
        {
            var listingCase = await _listingCaseRepository.GetListingCaseByIdAsync(listingCaseId);
            if(listingCase == null)
            {
                throw new KeyNotFoundException($"listing case with Id:{listingCaseId} not found");
            }
            ValidateListingCaseAccess(listingCase, userId, role, "access");
            var finalMediaSelection = await _listingCaseRepository.GetSelectedMediaIdsAsync(listingCaseId);
            return finalMediaSelection;
        }


        //Generate or return existing shareable link for listingCase
        public async Task<string> GenerateShareLinkAsync(string userId, int listingCaseId)
        {
            var listingCase = await _listingCaseRepository.GetListingCaseByIdAsync(listingCaseId);
            if (listingCase == null)
            {
                throw new KeyNotFoundException($"listing case with Id:{listingCaseId} not found");
            }

            if(!string.IsNullOrWhiteSpace(listingCase.ShareLinkUrl))
            {

                return listingCase.ShareLinkUrl;
            }

            var shareLink = await _listingCaseRepository.GenerateShareLinkAsync(listingCase);

            await SaveListingCaseEventAsync(
                      EventTypes.CASE_UPDATE,
                      $"listingCase:{listingCaseId} Share link :{shareLink}",
                      listingCaseId.ToString(),
                      userId,
                      true
                      );

            return shareLink;
        }

        //View the listingCase via shareable link with token validation
        public async Task<ListingCaseGetResponseDto> ViewShareLinkAsync(int listingCaseId, string token)
        {
            var listingCase = await _listingCaseRepository.GetListingCaseByIdAsync(listingCaseId);
            if (listingCase == null)
            {
                throw new KeyNotFoundException($"listing case with Id:{listingCaseId} not found");
            }
            if (listingCase.ShareLinkToken != token)
            {
                throw new UnauthorizedAccessException($"Invalid token");
            }

            return _mapper.Map<ListingCaseGetResponseDto>(listingCase);

        }


        //Validate user permission for ListingCase operations
        private void ValidateListingCaseAccess(ListingCase listingCase, string userId, string role, string operation = "access")
        {
            switch (role.ToLower())
            {
                case "admin":
                    // Admin can access/modify all listings
                    break;
                case "agent":
                    if (!listingCase.Agents.Any(a => a.Id == userId))
                    {
                        throw new UnauthorizedAccessException($"Agent can only {operation} listings assigned to them");
                    }
                    break;
                case "photographycompany":
                    if (listingCase.UserId != userId)
                    {
                        throw new UnauthorizedAccessException($"Photography company can only {operation} listings they created");
                    }
                    break;
                default:
                    throw new UnauthorizedAccessException($"Invalid role: {role}");
            }
        }


        //Validate status transition workflow
        private static void ValidateStatusTransition(ListcaseStatus currentStatus, ListcaseStatus newStatus)
        {
            var allowedTransitions = new Dictionary<ListcaseStatus, ListcaseStatus[]>
            {
                [ListcaseStatus.Created] = [ListcaseStatus.Pending],
                [ListcaseStatus.Pending] = [ListcaseStatus.Delivered, ListcaseStatus.Created],
                [ListcaseStatus.Delivered] = []
            };

            if (!allowedTransitions.ContainsKey(currentStatus) ||
                !allowedTransitions[currentStatus].Contains(newStatus))
            {
                throw new InvalidOperationException(
                    $"Invalid status transition from {currentStatus} to {newStatus}");
            }
        }


        //Get ListingCase by ID with null check
        private async Task<ListingCase> GetListingCaseWithValidationAsync(int listingCaseId)
        {
            // Get ListingCase by ID with validation (includes Agents, User)
            var listingCase = await _listingCaseRepository.GetListingCaseByIdAsync(listingCaseId);
            if (listingCase == null)
            {
                throw new KeyNotFoundException($"ListingCase with ID {listingCaseId} not found");
            }
            return listingCase;
        }


        private async Task<ListingCase> GetListingCaseDetailWithValidationAsync(int listingCaseId)
        {
            // Get ListingCase by ID with validation (includes Agents, CaseContacts, MediaAssets)
            var listingCase = await _listingCaseRepository.GetListingCaseDetailByIdAsync(listingCaseId);
            if (listingCase == null)
            {
                throw new KeyNotFoundException($"Id:{listingCaseId} listingCase does not exist");
            }
            return listingCase;
        }



       public async Task<MemoryStream> DownloadListingCaseMediaAsZipAsync(int listingCaseId, string userId, string role)
        {
            //Get ListingCase by ID with validation
            var listingCase = await GetListingCaseDetailWithValidationAsync(listingCaseId);

            //Validate ListingCase Access by check role
            ValidateListingCaseAccess(listingCase, userId, role, "access");

            //Get mediaAssets for the listingCase
            var mediaAssets = listingCase.MediaAssets.ToList();
          
            if (!mediaAssets.Any())
            {
                throw new KeyNotFoundException("No mediaAssets found");
            }

            //Zip Media content
            var zipStream = await _azureBlobStorageService.ZipMediaAssetsAsync(mediaAssets); 
            if(zipStream == null)
            {
                throw new InvalidOperationException("Failed to create ZIP file");
            }
               
            return zipStream;

        }

        private async Task SaveListingCaseEventAsync(string eventType,string eventMessage,
            string resourceId,string? operatorUserId, bool isSuccess)

        {
            var eventLog = new RecamEventLog
            {
                EventType = eventType,
                EventMessage = eventMessage,
                ResourceCaseId = resourceId,
                OperatorUserId = operatorUserId,
                IsSuccess = isSuccess
            };

            await _mongoDbContext.ListingCaseEventLogs.InsertOneAsync(eventLog);
           
        }

    }
}

