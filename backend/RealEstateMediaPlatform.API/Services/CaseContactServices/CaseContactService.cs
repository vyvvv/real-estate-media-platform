using AutoMapper;
using RealEstateMediaPlatform.API.DTOs.CaseContact;
using RealEstateMediaPlatform.API.Repositories.CaseContactRepositories;

namespace RealEstateMediaPlatform.API.Services.CaseContactServices
{
    public class CaseContactService : ICaseContactService
    {

        private readonly ICaseContactRepository _CaseContactRepository;
        private readonly IMapper _mapper;
        public CaseContactService (ICaseContactRepository caseContactRepository, IMapper mapper )
        {
            _CaseContactRepository  = caseContactRepository;
            _mapper = mapper;
        }

        public async Task<CaseContactCreateResponseDto> CreateCaseContactAsync(CaseContactCreateRequestDto caseContactCreateRequestDto)
        {

            var caseContact = await _CaseContactRepository.CreateCaseContactAsync(caseContactCreateRequestDto);
            return _mapper.Map<CaseContactCreateResponseDto>(caseContact);

        }

    }
}
