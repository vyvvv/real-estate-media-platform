using System.ComponentModel.DataAnnotations;

namespace RealEstate.API.DTOs.Listings;

public class CreateListingCaseDto
{
    //public string UserId { get; set; } // This will be set from the authenticated user context, not from the client input

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

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

    [Range(0, double.MaxValue)]
    public double Price { get; set; }

    [Range(0, 20)]
    public int Bedrooms { get; set; }

    [Range(0, 20)]
    public int Bathrooms { get; set; }

    [Range(0, 20)]
    public int Garages { get; set; }

    [Range(0, double.MaxValue)]
    public double FloorArea { get; set; }

    public int PropertyType { get; set; }

    public int SaleCategory { get; set; }
}