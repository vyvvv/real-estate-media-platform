using System.ComponentModel.DataAnnotations;

namespace RealEstateMediaPlatform.API.DTOs.User
{
    public class UserUpdatePasswordRequestDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
    }
}
