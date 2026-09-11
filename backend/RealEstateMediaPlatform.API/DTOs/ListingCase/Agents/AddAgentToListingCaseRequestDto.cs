using System.ComponentModel.DataAnnotations;

namespace RealEstateMediaPlatform.API.DTOs.ListingCase.Agents
{
    public class AddAgentToListingCaseRequestDto
    {
        [Required]
        public int ListingCaseId { get; set; }

        [Required]
        public string AgentId { get; set; }

    }
}
