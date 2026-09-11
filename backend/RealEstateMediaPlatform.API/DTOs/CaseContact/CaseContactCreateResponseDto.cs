namespace RealEstateMediaPlatform.API.DTOs.CaseContact
{
    public class CaseContactCreateResponseDto
    {

        public int ContactId { get; set; }
        public string FirstName { get; set; }      
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string ProfileUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
