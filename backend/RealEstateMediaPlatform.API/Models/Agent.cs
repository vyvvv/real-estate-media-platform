using System.ComponentModel.DataAnnotations;

namespace RealEstateMediaPlatform.API.Models
{
    public class Agent:User
    {
        [Required]
        [StringLength(50)]
        public string AgentFirstName {  set; get; }
        
        [Required]
        [StringLength(50)]
        public string AgentLastName { set; get; }

        public string? AvatarUrl { set; get; }

        public string? CompanyName {  set; get; }
        public List<ListingCase> ListingCases { set; get; } = new List<ListingCase>();
        public List<PhotographyCompany> PhotographyCompanys { set; get; } = new List<PhotographyCompany>();

    }
}
