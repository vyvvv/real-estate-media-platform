namespace RealEstate.API.DTOs.Listings;

public class ListingCaseListItemDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public int Postcode { get; set; }

    public double Price { get; set; }

    public int Bedrooms { get; set; }

    public int Bathrooms { get; set; }

    public int Garages { get; set; }

    public int PropertyType { get; set; }

    public int SaleCategory { get; set; }

    public int ListingCaseStatus { get; set; }

    public DateTime CreatedAt { get; set; }
}