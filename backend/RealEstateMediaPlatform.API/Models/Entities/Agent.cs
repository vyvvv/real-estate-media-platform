using System;

namespace RealEstateMediaPlatform.API.Models.Entities;

public class Agent
{
    public required string Id {get;set;}
    public required string AgentFirstName {get;set;}
    public required string AgentLastName {get;set;}
    public required string AvatarUrl {get;set;}
    public required string CompanyName {get;set;}



}
