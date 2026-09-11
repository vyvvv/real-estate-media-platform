using System.ComponentModel.DataAnnotations;

namespace RealEstateMediaPlatform.API.DTOs.CaseContact
{
    public class CaseContactCreateRequestDto
    {
     
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string ProfileUrl { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
      


    }
}
