using RealEstateMediaPlatform.API.DTOs.User.IUser;

namespace RealEstateMediaPlatform.API.DTOs.Agent
{
    public class AgentRegisterResponseDto: IUserRegisterResponseDto
    {   
        public string Id { get; set; }
      
        public string AgentFirstName { set; get; }

        public string AgentLastName { set; get; }

        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
