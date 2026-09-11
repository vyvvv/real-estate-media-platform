using Microsoft.AspNetCore.Identity;

namespace RealEstateMediaPlatform.API.Models
{
    public class User : IdentityUser
    {
        public bool IsDeleted { set; get; } = false;
        public DateTime CreatedAt { set; get; } = DateTime.Now;
        public List <ListingCase> ListingCase { get; set; } = new List <ListingCase> ();

    }
}
