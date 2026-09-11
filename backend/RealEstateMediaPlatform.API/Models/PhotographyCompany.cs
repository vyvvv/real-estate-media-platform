using System.ComponentModel.DataAnnotations;

namespace RealEstateMediaPlatform.API.Models
{
    public class PhotographyCompany:User
    {
      
        public string PhotographyCompanyName {  set; get; }
        public List<Agent> Agents { get; set; } = new List<Agent>();

    }
}
