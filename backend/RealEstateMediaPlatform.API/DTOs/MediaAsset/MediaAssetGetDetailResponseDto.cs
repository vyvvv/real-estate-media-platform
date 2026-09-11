using RealEstateMediaPlatform.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateMediaPlatform.API.DTOs.MediaAsset
{
    public class MediaAssetGetDetailResponseDto
    {

        public int Id { get; set; }
        public MediaAssetType MediaType { get; set; }
        public string MediaUrl { get; set; }
        public DateTime UploadedAt { get; set; }

        public bool IsSelect { get; set; }
        public bool IsHero { get; set; }

        public int ListingCaseId { get; set; }
        public string UserId { get; set; }

    }
}
