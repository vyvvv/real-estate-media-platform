using System;

namespace RealEstateMediaPlatform.API.Models.Entities;

public class CaseContact
{
    public required int ContactId {get; set;}
    public required string FirstName {get;set;}
    public required string LastName {get;set;}
    public required string ProfileUrl {get;set;}
    public required string CompanyName {get;set;}
    public required string Email {get;set;}
    public required string PhoneNumber {get;set;}
    public required int ListingCaseId {get;set;}

    public required ListingCase listingCase{get;set;}

}
