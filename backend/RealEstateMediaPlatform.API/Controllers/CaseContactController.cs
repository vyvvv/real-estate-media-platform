using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RealEstateMediaPlatform.API.Data;


namespace RealEstateMediaPlatform.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseContactController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CaseContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCaseContacts()
        {
            var contacts = await _context.CaseContacts.Select(c => new
            {
                c.ContactId,
                c.FirstName,
                c.LastName,
                c.CompanyName,
                c.ProfileUrl,
                c.Email,
                c.PhoneNumber,
                c.ListingCaseId
            }).ToListAsync();

            return Ok(contacts);
        }
    }
}
