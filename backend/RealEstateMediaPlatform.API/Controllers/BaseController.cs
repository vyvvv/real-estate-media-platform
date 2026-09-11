using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RealEstateMediaPlatform.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        //Get current user ID and role from JWT token
        protected (string userId, string role) GetCurrentUserInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            var role = roles.FirstOrDefault(); // Currently supports only one role per user

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {
                throw new UnauthorizedAccessException("User ID or role not found");
            }

            return (userId, role);
        }
    }
}
