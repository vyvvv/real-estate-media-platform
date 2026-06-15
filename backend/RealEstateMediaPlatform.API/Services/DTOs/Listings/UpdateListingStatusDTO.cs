using System.ComponentModel.DataAnnotations;

namespace RealEstate.API.DTOs.Listings;

public class UpdateListingCaseStatusDto
{
    [Required]
    public int ListingCaseStatus { get; set; }
}