namespace RealEstateMediaPlatform.API.DTOs.User.IUser
{
    public interface IUserRegisterRequestDto
    {
        string Email { get; set; }
        string ?Password { get; set; }
    }
}