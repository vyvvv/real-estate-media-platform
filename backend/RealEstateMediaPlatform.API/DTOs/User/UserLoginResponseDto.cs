namespace RealEstateMediaPlatform.API.DTOs.User
{
    public class UserLoginResponseDto
    {
        public string Id {  get; set; } = string.Empty; //代表用户在系统中的唯一标识符，通常用于区别不同的用户
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty; //身份验证令牌，让系统在需要登录才能访问的页面，每次都能认得用户。

        public List<string> Roles { get; set; } = new List<string>();

        public DateTime LoginTime { get; set; } = DateTime.UtcNow; //统一时间：避免地区，春令时/冬令时时间不同带来的问题
    }
}
