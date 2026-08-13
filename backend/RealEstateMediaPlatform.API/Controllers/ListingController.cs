using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.API.DTOs.Listings;
using RealEstateMediaPlatform.API.Data;
using RealEstateMediaPlatform.API.Models.Entities;
using RealEstateMediaPlatform.API.Models.Enums;

namespace RealEstateMediaPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ListingsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetListings()
    {
        var listings = await _context.ListingCases
            .Where(l => !l.IsDeleted)
            .OrderByDescending(l => l.CreatedAt)
            .Select(l => new
            {
                l.Id,
                l.Title,
                l.Description,
                l.Street,
                l.City,
                l.State,
                l.Postcode,
                l.Longitude,
                l.Latitude,
                l.Price,
                l.Bedrooms,
                l.Bathrooms,
                l.Garages,
                l.FloorArea,
                l.CreatedAt,
                l.IsDeleted,
                l.PropertyType,
                l.SaleCategory,
                ListingCaseStatus = l.ListCaseStatus,
                l.UserId
            })
            .ToListAsync();

        return Ok(listings);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetListingById(int id)
    {
        var listingCase = await _context.ListingCases
            .FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted);
            
           
        if (listingCase == null)
        {
            return NotFound("Listing case not found.");
        }

        return Ok(listingCase);
    }

    [HttpPost]
    public async Task<ActionResult> CreateListing([FromBody] CreateListingDto dto)
{
    if (dto == null)
    {
        return BadRequest("Listing data cannot be null.");
    }

    var photographyCompany = await _context.PhotographyCompanies.FirstOrDefaultAsync();

    if (photographyCompany == null)
    {
        return BadRequest("No photography company found.");
    }

    var owner = await _context.Users.FindAsync(photographyCompany.Id);

    if (owner == null)
    {
        return BadRequest("Photography company user not found.");
    }

    var listingCase = new ListingCase
    {
        Title = dto.Title,
        Description = dto.Description,
        Street = dto.Street,
        City = dto.City,
        State = dto.State,
        Postcode = dto.Postcode,
        Longitude = dto.Longitude,
        Latitude = dto.Latitude,
        Price = dto.Price,
        Bedrooms = dto.Bedrooms,
        Bathrooms = dto.Bathrooms,
        Garages = dto.Garages,
        FloorArea = dto.FloorArea,

        PropertyType = (PropertyType)dto.PropertyType,
        SaleCategory = (SaleCategory)dto.SaleCategory,

        UserId = photographyCompany.Id,
        User = owner,
        CreatedAt = DateTime.UtcNow,
        IsDeleted = false,
        ListCaseStatus = ListCaseStatus.Created
    };

    _context.ListingCases.Add(listingCase);
    await _context.SaveChangesAsync();

    return Ok(new
    {
        message = "Listing Case created successfully",
        listingCaseId = listingCase.Id
    });
}

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatingListing(int id,[FromBody]UpdateListingDto dto)
    {
    var existing = await _context.ListingCases
        .FirstOrDefaultAsync(listing =>
            listing.Id == id &&
            !listing.IsDeleted);

    if (existing == null)
    {
        return NotFound("Listing case not found.");
    }

    if (!Enum.IsDefined(
        typeof(PropertyType),
        dto.PropertyType))
    {
        return BadRequest("Invalid property type.");
    }

    if (!Enum.IsDefined(
        typeof(SaleCategory),
        dto.SaleCategory))
    {
        return BadRequest("Invalid sale category.");
    }

    existing.Title = dto.Title.Trim();
    existing.Description = dto.Description?.Trim() ?? "";
    existing.Street = dto.Street.Trim();
    existing.City = dto.City.Trim();
    existing.State = dto.State.Trim();
    existing.Postcode = dto.Postcode;

    existing.Longitude = dto.Longitude;
    existing.Latitude = dto.Latitude;

    existing.Price = dto.Price;
    existing.Bedrooms = dto.Bedrooms;
    existing.Bathrooms = dto.Bathrooms;
    existing.Garages = dto.Garages;
    existing.FloorArea = dto.FloorArea;

    existing.PropertyType =
        (PropertyType)dto.PropertyType;

    existing.SaleCategory =
        (SaleCategory)dto.SaleCategory;

    await _context.SaveChangesAsync();

    return Ok(new
    {
        message = "Listing Case updated successfully",
        listingCaseId = existing.Id
    });
}

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteListing(int id)
    {
        var listingCase = await _context.ListingCases.FindAsync(id);
        if(listingCase ==null || listingCase.Id <= 0)
        {
            return NotFound("listing case not found.");
        }
        listingCase.IsDeleted = true;
        //_context.ListingCases.Remove(listingCase);
        await _context.SaveChangesAsync();
        return Ok(new {message = "Listing Case deleted successfully", listingCaseId = listingCase.Id});
        
    }

    [HttpGet("deleted")]
    public async Task<IActionResult> GetDeletedListings()
    {
        var listings = await _context.ListingCases.Where(x=>x.IsDeleted == true).ToListAsync();
    

        if (!listings.Any() )
        {
            return NotFound("No deleted listings found");
        }

        return Ok(listings);
    }

    [HttpPatch("{id}/restore")]
    public async Task<IActionResult> RestoreListing(int id)
    {
        var listingCase = await _context.ListingCases.Where(x=>x.Id ==id && x.IsDeleted == true).FirstOrDefaultAsync();
        if (listingCase == null)
        {
            return NotFound("listing case not found or not deleted");
        }
        listingCase.IsDeleted = false;
        await _context.SaveChangesAsync();
        return Ok(new { message = "Listing case restored successfully", listingCaseId = listingCase.Id });

    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateListingStatus(int id, [FromBody]  UpdateListingCaseStatusDto request)
    {
        var listingCase = await _context.ListingCases.FindAsync(id);
        if (listingCase == null || listingCase.IsDeleted)
        {
            return NotFound("Listing case not found.");
        }

        listingCase.ListCaseStatus = (ListCaseStatus)request.ListingCaseStatus;
        await _context.SaveChangesAsync();
        return Ok(new { message = "Listing case status updated successfully", listingCaseId = listingCase.Id ,   status = listingCase.ListCaseStatus});
    }

}


