namespace RealEstate.API.DTOs.Listings;

public class ListingCaseDetailDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Street { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public int Postcode { get; set; }

    public decimal Longitude { get; set; }

    public decimal Latitude { get; set; }

    public double Price { get; set; }

    public int Bedrooms { get; set; }

    public int Bathrooms { get; set; }

    public int Garages { get; set; }

    public double FloorArea { get; set; }

    public int PropertyType { get; set; }

    public int SaleCategory { get; set; }

    public int ListingCaseStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public string UserId { get; set; } = string.Empty;

    // public List<MediaAssetDto> MediaAssets { get; set; } = new();

    // public List<CaseContactDto> CaseContacts { get; set; } = new();

    // public List<AgentDto> AssignedAgents { get; set; } = new();
}