using AutoMapper;
using Recam.Data;
using Recam.DTOs.CaseContact;
using Recam.Models;

namespace Recam.Repositories.CaseContactRepositories
{
    public class CaseContactRepository: ICaseContactRepository
    {

        private readonly RecamDbContext _dbContext;
        private readonly IMapper _mapper;

        public CaseContactRepository(RecamDbContext dbContext, IMapper mapper)
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
