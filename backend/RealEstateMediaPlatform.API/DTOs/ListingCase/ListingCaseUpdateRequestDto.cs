using RealEstateMediaPlatform.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace RealEstateMediaPlatform.API.DTOs.ListingCase
{
    public class ListingCaseUpdateRequestDto
    {      
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int Postcode { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public double Price { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Garages { get; set; }
        public double FloorArea { get; set; }

        [EnumDataType(typeof(PropertyType))]
        public PropertyType? PropertyType { get; set; }

        [EnumDataType(typeof(SaleCategory))]
        public SaleCategory? SaleCategory { get; set; }

        [EnumDataType(typeof(ListcaseStatus))]
        public ListcaseStatus? ListcaseStatus { get; set; }
    }
}
