using RealEstateMediaPlatform.API.DTOs.Agent;
using RealEstateMediaPlatform.API.DTOs.CaseContact;
using RealEstateMediaPlatform.API.DTOs.MediaAsset;
using RealEstateMediaPlatform.API.Enums;


namespace RealEstateMediaPlatform.API.DTOs.ListingCase
{
    public class ListingCaseGetDetailResponseDto
    {
        public int Id { get; set; }
        public string ?Title { get; set; }
        public string ?Description { get; set; }
        public string ?Street { get; set; }
        public string ?City { get; set; }
        public string ?State { get; set; }
        public int Postcode { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public double Price { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Garages { get; set; }
        public double FloorArea { get; set; }
        public DateTime CreatedAt { get; set; }
        public PropertyType PropertyType { get; set; }
        public SaleCategory SaleCategory { get; set; }
        public ListcaseStatus ListcaseStatus { get; set; }

        public List<AgentGetDetailResponseDto> Agents { get; set; } = new List<AgentGetDetailResponseDto>();
        public List<CaseContactGetDetailResponseDto> CaseContacts { get; set; } = new List<CaseContactGetDetailResponseDto>();
        public List<MediaAssetGetDetailResponseDto> MediaAssets { get; set; } = new List<MediaAssetGetDetailResponseDto>();



    }
}
