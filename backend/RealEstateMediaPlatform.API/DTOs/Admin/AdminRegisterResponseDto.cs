using RealEstateMediaPlatform.API.DTOs.User.IUser;
using System.ComponentModel.DataAnnotations;


namespace RealEstateMediaPlatform.API.DTOs.Admin
{
    public class AdminRegisterResponseDto: IUserRegisterResponseDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }   
}
