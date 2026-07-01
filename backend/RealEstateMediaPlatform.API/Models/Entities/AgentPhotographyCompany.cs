using System;

namespace RealEstateMediaPlatform.API.Models.Entities;
// 这个类是一个中间表，用于表示Agent和PhotographyCompany之间的多对多关系。每个Agent可以关联多个PhotographyCompany，每个PhotographyCompany也可以关联多个Agent。
public class AgentPhotographyCompany
{
    public required string AgentId{get;set;}

    public required string PhotographyCompanyId{get;set;}


    public Agent Agent { get; set; }
    public PhotographyCompany PhotographyCompany { get; set; }

}
