using System;

namespace RealEstateMediaPlatform.API.Models.Entities;

// id不需要required，EF会自动生成。此外，ProfileUrl可以为空，因为有些联系人可能没有头像。
// 其次，CompanyName、Email和PhoneNumber都是必填字段，因为这些信息对于联系客户是必要的。最后，ListingCaseId是必填字段，因为每个联系人都必须关联到一个ListingCase。
public class CaseContact
{
    public int ContactId {get; set;}
    public required string FirstName {get;set;}
    public required string LastName {get;set;}
    public string? ProfileUrl {get;set;}
    public required string CompanyName {get;set;}
    public required string Email {get;set;}
    public required string PhoneNumber {get;set;}
    public int ListingCaseId {get;set;}

    public ListingCase ListingCase{get;set;}

}
