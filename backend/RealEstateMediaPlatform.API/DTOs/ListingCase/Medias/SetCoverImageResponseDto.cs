using RealEstateMediaPlatform.API.Enums;

namespace RealEstateMediaPlatform.API.DTOs.ListingCase.Medias
{
    public class SetCoverImageResponseDto
    {          
        public int Id { get; set; }
        public MediaAssetType MediaType { get; set; }
        public string MediaUrl { get; set; }
        public bool IsSelect { get; set; }
        public bool IsHero { get; set; }
        public int ListingCaseId { get; set; }
        public string UserId { get; set; }
   
    }
}
