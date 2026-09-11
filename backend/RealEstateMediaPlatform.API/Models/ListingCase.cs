using RealEstateMediaPlatform.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateMediaPlatform.API.Models
{
    public class ListingCase
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string ?Title { get; set; }
        public string ?Description { get; set; }
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
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
      
        public string? ShareLinkUrl { get; set; }
        public string? ShareLinkToken { get; set; }
        public DateTime? ShareLinkGeneratedAt { get; set; }

        public PropertyType PropertyType { get; set; }
        public SaleCategory SaleCategory { get; set; }
        public ListcaseStatus ListcaseStatus { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<Agent> Agents { get; set; } = new List<Agent>();
        public List<CaseContact> CaseContacts { get; set; } = new List<CaseContact>();
        public List<MediaAsset> MediaAssets { get; set; } = new List<MediaAsset>();


    }
}
