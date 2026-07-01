using System;
using RealEstateMediaPlatform.API.Models.Enums;

namespace RealEstateMediaPlatform.API.Models.Entities;


public class ListingCase
{
    public int Id {get; set;}
    public required string Title {get; set;}
    public required string Description {get; set;}
    public required string Street {get; set;}
    public required string City {get; set;}
    public required string State {get; set;}

    public int Postcode {get; set;}  

    public decimal Longitude {get; set;}
    public decimal Latitude {get; set;}

    public double Price {get; set;}

    public int Bedrooms {get; set;}

    public int Bathrooms {get; set;}

    public int Garages {get; set;}

    public double FloorArea {get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public bool IsDeleted {get; set;} = false;

    public PropertyType PropertyType {get; set;}
    public SaleCategory SaleCategory {get; set;}
    
    public ListCaseStatus ListCaseStatus {get; set;}
 
    public required string UserId {get; set;}
    public required ApplicationUser User { get; set; }
    public ICollection<AgentListingCase> AgentListingCases { get; set; } = new List<AgentListingCase>();

}

