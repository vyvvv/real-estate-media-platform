using Microsoft.AspNetCore.Mvc;

namespace RealEstateMediaPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetListings()
    {
        var listings = new[]
        {
    new
    {
        Id = 1,
        Title = "Modern Family House",
        Description = "A beautiful house near the beach.",
        Street = "93 Beach Road",
        City = "Melbourne",
        State = "VIC",
        Postcode = 3000,
        Longitude = 144.9631m,
        Latitude = -37.8136m,
        Price = 800000,
        Bedrooms = 3,
        Bathrooms = 2,
        Garages = 1,
        FloorArea = 180,
        CreatedAt = DateTime.Parse("2026-06-16T00:00:00"),
        IsDeleted = false,
        PropertyType = 0,
        SaleCategory = 0,
        ListingCaseStatus = 0,
        UserId = "admin-user-id"
    }
};


        return Ok(listings);
    }
}