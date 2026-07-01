using System;

namespace RealEstateMediaPlatform.API.Models.Entities;
// 这个类是一个中间表，用于表示Agent和ListingCase之间的多对多关系。每个Agent可以关联多个ListingCase，每个ListingCase也可以关联多个Agent。
// 通过这个中间表，我们可以方便地查询某个Agent关联的所有ListingCase，或者某个ListingCase关联的所有Agent。
public class AgentListingCase
{
    
    public int ListingCaseId { get; set; }
    public required string AgentId{get;set;}


    public ListingCase ListingCase { get; set; }
    public Agent Agent{get;set;}


}
