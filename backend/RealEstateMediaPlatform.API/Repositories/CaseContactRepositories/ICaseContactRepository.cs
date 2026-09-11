using RealEstateMediaPlatform.API.DTOs.CaseContact;
using RealEstateMediaPlatform.API.Models;

namespace RealEstateMediaPlatform.API.Repositories.CaseContactRepositories
{
    public interface ICaseContactRepository
    {
        Task<CaseContact> CreateCaseContactAsync(CaseContactCreateRequestDto caseContactCreateRequestDto);
    }
}
