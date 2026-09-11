using RealEstateMediaPlatform.API.Enums;

namespace RealEstateMediaPlatform.API.DTOs.MediaAsset
{
    public class MediaAssetGroupedResponseDto
    {
        public Dictionary<MediaAssetType, List<MediaAssetGetDetailResponseDto>> MediaAssetsByType { get; set; }= new();
         
        public int TotalCount { get; set; }
    }
}
