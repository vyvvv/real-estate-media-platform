using Microsoft.AspNetCore.Identity;

namespace RealEstateMediaPlatform.API.Models.Entities;

    public class ApplicationUser : IdentityUser
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
