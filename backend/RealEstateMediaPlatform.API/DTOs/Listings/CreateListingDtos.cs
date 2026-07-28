using System.ComponentModel.DataAnnotations;

namespace RealEstate.API.DTOs.Listings;

public class CreateListingDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

[Required]
    public string Street { get; set; } = string.Empty;

    [Required]
    public string City { get; set; } = string.Empty;
    [Required]
    public string State { get; set; } = string.Empty;
    [Required]
    public int Postcode { get; set; }

    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }

[Required]
    public double Price { get; set; }

    [Required]
    public int Bedrooms { get; set; }
    [Required]
    public int Bathrooms { get; set; }
    [Required]
    public int Garages { get; set; }
    [Required]
    public double FloorArea { get; set; }

    [Required]
    public int PropertyType { get; set; }

    [Required]
    public int SaleCategory { get; set; }
}