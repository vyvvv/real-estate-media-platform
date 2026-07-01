using System;

namespace RealEstateMediaPlatform.API.Models.Entities;

public class PhotographyCompany
{
    public required string Id {get;set;}
    public required string PhotographyCompanyName {get;set;}

    public ICollection<AgentPhotographyCompany> AgentPhotographyCompanies {get;set;} = new List <AgentPhotographyCompany>();


}
