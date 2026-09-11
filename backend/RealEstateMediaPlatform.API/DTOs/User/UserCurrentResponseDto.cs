using Microsoft.AspNetCore.Identity;
using RealEstateMediaPlatform.API.DTOs.ListingCase;

namespace RealEstateMediaPlatform.API.DTOs.User
{
    public class UserCurrentResponseDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public List<int> ListingCaseIds { get; set; } = new List<int>();
    }
}
