using RealEstateMediaPlatform.API.DTOs.User.IUser;
using System.ComponentModel.DataAnnotations;

namespace RealEstateMediaPlatform.API.DTOs.PhotographyCompany
{
    public class PhotographyCompanyRegisterResponseDto: IUserRegisterResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string PhotographyCompanyName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
      
    }
}
