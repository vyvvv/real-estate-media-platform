using System.Text.Json.Serialization;

namespace RealEstateMediaPlatform.API.DTOs.Agent
{
    public class AgentGetDetailResponseDto
    {
        public string Id { get; set; }
        [JsonIgnore]
        public string AgentFirstName { get; set; }
        [JsonIgnore]
        public string AgentLastName { get; set; }
        public string FullName => $"{AgentFirstName} {AgentLastName}";
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarUrl { get; set; }
        public string CompanyName { get; set; }
      
    }
}
