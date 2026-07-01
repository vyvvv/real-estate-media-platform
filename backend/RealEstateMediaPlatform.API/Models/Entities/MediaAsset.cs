using System;
using RealEstateMediaPlatform.API.Models.Enums;

namespace RealEstateMediaPlatform.API.Models.Entities;

public class MediaAsset
{
    public int Id { get; set; }
    public MediaType MediaType { get; set; }
    public required string MediaUrl { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    public bool IsSelected { get; set; }

    public bool IsHero { get; set; }
    public bool IsDeleted { get; set; }

    public int ListingCaseId { get; set; }
    public required string UserId { get; set; }
    public ListingCase ListingCase { get; set; }
    public ApplicationUser User { get; set; }

}
