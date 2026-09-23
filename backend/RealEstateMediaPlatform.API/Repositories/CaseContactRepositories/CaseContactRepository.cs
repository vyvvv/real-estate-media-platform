using AutoMapper;
using RealEstateMediaPlatform.API.Data;
using RealEstateMediaPlatform.API.DTOs.CaseContact;
using RealEstateMediaPlatform.API.Models;

namespace RealEstateMediaPlatform.API.Repositories.CaseContactRepositories
{
    public class CaseContactRepository: ICaseContactRepository
    {

        private readonly RealEstateDbContext _dbContext;
        private readonly IMapper _mapper;

        public CaseContactRepository(RealEstateDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }
        public async Task<CaseContact> CreateCaseContactAsync(CaseContactCreateRequestDto caseContactCreateRequestDto)
        {
            var caseContact = _mapper.Map<CaseContact>(caseContactCreateRequestDto);

            await _dbContext.CaseContacts.AddAsync(caseContact);
            await _dbContext.SaveChangesAsync();
            return caseContact;
        }
    }
}
