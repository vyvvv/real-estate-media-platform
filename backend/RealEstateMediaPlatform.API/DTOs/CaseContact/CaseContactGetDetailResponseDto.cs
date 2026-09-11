using System.Text.Json.Serialization;

namespace RealEstateMediaPlatform.API.DTOs.CaseContact
{
    public class CaseContactGetDetailResponseDto
    {
        public int ContactId { get; set; }

        [JsonIgnore]
        public string FirstName { get; set; }

        [JsonIgnore]
        public string LastName { get; set; }
        public string FullName => $"{FirstName}{LastName}";
        public string CompanyName { get; set; }
        public string ProfileUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
     
    }
}
