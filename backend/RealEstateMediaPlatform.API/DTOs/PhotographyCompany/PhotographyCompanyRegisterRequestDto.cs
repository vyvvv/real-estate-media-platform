using RealEstateMediaPlatform.API.DTOs.User.IUser;
using System.ComponentModel.DataAnnotations;

namespace RealEstateMediaPlatform.API.DTOs.PhotographyCompany
{
    public class PhotographyCompanyRegisterRequestDto: IUserRegisterRequestDto
    {

        [Required]
        public string PhotographyCompanyName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;


    }
}
