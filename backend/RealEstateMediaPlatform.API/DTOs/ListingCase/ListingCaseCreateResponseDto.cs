using RealEstateMediaPlatform.API.Enums;

namespace RealEstateMediaPlatform.API.DTOs.ListingCase
{
    public class ListingCaseCreateResponseDto
    {
        public int Id { get; set; }
        public string ?Title { get; set; }    
        public string? Description { get; set; }   
        public int Bedrooms { get; set; } = 0;
        public int Bathrooms { get; set; } = 0;
        public int Garages { get; set; } = 0;
        public double FloorArea { get; set; } = 0;  
        public PropertyType PropertyType { get; set; }
        public SaleCategory SaleCategory { get; set; }
        public ListcaseStatus ListcaseStatus { get; set; }

    }
}
