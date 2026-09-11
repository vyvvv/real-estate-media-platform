using RealEstateMediaPlatform.API.DTOs.User.IUser;
using System.ComponentModel.DataAnnotations;

namespace RealEstateMediaPlatform.API.DTOs.Agent
{
    public class AgentRegisterRequestDto: IUserRegisterRequestDto
    {

        [Required]
        public string AgentFirstName { set; get; } = string.Empty;

        [Required]
        public string AgentLastName { set; get; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        public string? Password { get; set; } = null;
    }
}
