using RealEstateMediaPlatform.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateMediaPlatform.API.DTOs.MediaAsset
{
    public class MediaAssetCreateRequestDto
    {
        public MediaAssetType MediaType { get; set; }
        public string MediaUrl { get; set; }
        public int ListingCaseId { get; set; }
        public string UserId { get; set; }
    }
}
