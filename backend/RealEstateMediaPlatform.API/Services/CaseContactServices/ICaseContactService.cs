using RealEstateMediaPlatform.API.DTOs.CaseContact;

namespace RealEstateMediaPlatform.API.Services.CaseContactServices
{
    public interface ICaseContactService
    {

        Task<CaseContactCreateResponseDto> CreateCaseContactAsync(CaseContactCreateRequestDto caseContactCreateRequestDto);

    }
}
