using RealEstateMediaPlatform.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace RealEstateMediaPlatform.API.DTOs.ListingCase
{
    public class ListingCaseCreateRequestDto
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }

        public int Postcode { get; set; }

        public double Price { get; set; }

        [Required]
        public int Bedrooms { get; set; } = 0;

        [Required]
        public int Bathrooms { get; set; } = 0;

        [Required]
        public int Garages { get; set; } = 0;
        [Required]
        public double FloorArea { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public PropertyType PropertyType { get; set; }
        [Required]
        public SaleCategory SaleCategory { get; set; }

        public ListcaseStatus ListcaseStatus { get; set; }



    }
}
