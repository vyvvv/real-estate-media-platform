using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateMediaPlatform.API.Models
{
    public class CaseContact
    {
        [Key]
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string ProfileUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public int? ListingCaseId { get; set; }
        [ForeignKey("ListingCaseId")]
        public ListingCase? ListingCase { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
