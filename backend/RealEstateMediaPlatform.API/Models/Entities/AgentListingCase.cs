using System;

namespace RealEstateMediaPlatform.API.Models.Entities;

public class AgentListingCase
{
    
    public required int ListingCaseId { get; set; }
    public required string AgentId{get;set;}


    public required ListingCase listingCase { get; set; }
    public required Agent agent{get;set;}


}
