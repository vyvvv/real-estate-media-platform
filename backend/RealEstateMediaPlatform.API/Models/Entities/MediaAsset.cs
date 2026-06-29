using System;
using RealEstateMediaPlatform.API.Models.Enums;

namespace RealEstateMediaPlatform.API.Models.Entities;

public class MediaAsset
{
    public int Id { get; set; }
    public MediaType MediaType { get; set; }
    public string MediaUrl { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    public bool Isselect { get; set; }

    public bool IsHero { get; set; }
    public bool IsDeleted { get; set; }

    public required int ListingCaseId { get; set; }
    public int UserId { get; set; }
    public required ListingCase listingCase { get; set; }
    public required ApplicationUser User { get; set; }

}
