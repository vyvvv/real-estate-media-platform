using AutoMapper;
using RealEstateMediaPlatform.API.DTOs.Admin;
using RealEstateMediaPlatform.API.DTOs.Agent;
using RealEstateMediaPlatform.API.DTOs.CaseContact;
using RealEstateMediaPlatform.API.DTOs.ListingCase;
using RealEstateMediaPlatform.API.DTOs.ListingCase.Medias;
using RealEstateMediaPlatform.API.DTOs.MediaAsset;
using RealEstateMediaPlatform.API.DTOs.PhotographyCompany;
using RealEstateMediaPlatform.API.DTOs.User;
using RealEstateMediaPlatform.API.DTOs.User.IUser;
using RealEstateMediaPlatform.API.Models;

namespace RealEstateMediaPlatform.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Agent, AgentRegisterResponseDto>();
            CreateMap<Agent, AgentGetDetailResponseDto>();
            CreateMap<PhotographyCompany, PhotographyCompanyRegisterResponseDto>();

            CreateMap<User, AdminRegisterResponseDto>();
            CreateMap<User, UserCurrentResponseDto>();


            CreateMap<User, UserLoginResponseDto>()
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.LoginTime, opt => opt.Ignore());

            CreateMap<CaseContact, CaseContactGetDetailResponseDto>();
            CreateMap<MediaAsset, MediaAssetGetDetailResponseDto>();

            CreateMap<ListingCaseCreateRequestDto, ListingCase>();
            CreateMap<ListingCase, ListingCaseCreateResponseDto>();
            CreateMap<ListingCase, ListingCaseGetResponseDto>();

            CreateMap<ListingCase, ListingCaseGetDetailResponseDto>();

            CreateMap<MediaAsset,SetCoverImageResponseDto>();


            CreateMap<CaseContact, CaseContactCreateResponseDto>();
            CreateMap<CaseContactCreateRequestDto, CaseContact>();

            CreateMap<PhotographyCompanyRegisterRequestDto, PhotographyCompany>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<AgentRegisterRequestDto, Agent>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<IUserRegisterRequestDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<ListingCaseUpdateRequestDto, ListingCase>()
                .ForMember(dest => dest.Title, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Title)))
                .ForMember(dest => dest.Description, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Description)))
                .ForMember(dest => dest.Street, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Street)))
                .ForMember(dest => dest.City, opt => opt.Condition(src => !string.IsNullOrEmpty(src.City)))
                .ForMember(dest => dest.State, opt => opt.Condition(src => !string.IsNullOrEmpty(src.State)))
                .ForMember(dest => dest.Postcode, opt => opt.Condition(src => src.Postcode != 0))
                .ForMember(dest => dest.Price, opt => opt.Condition(src => src.Price != 0))
                .ForMember(dest => dest.Bedrooms, opt => opt.Condition(src => src.Bedrooms != 0))
                .ForMember(dest => dest.Bathrooms, opt => opt.Condition(src => src.Bathrooms != 0))
                .ForMember(dest => dest.Garages, opt => opt.Condition(src => src.Garages != 0))
                .ForMember(dest => dest.FloorArea, opt => opt.Condition(src => src.FloorArea != 0))
                .ForMember(dest => dest.Longitude, opt => opt.Condition(src => src.Longitude != 0))
                .ForMember(dest => dest.Latitude, opt => opt.Condition(src => src.Latitude != 0))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Agents, opt => opt.Ignore())
                .ForMember(dest => dest.CaseContacts, opt => opt.Ignore())
                .ForMember(dest => dest.MediaAssets, opt => opt.Ignore())
                .ForMember(dest => dest.PropertyType, opt => opt.Condition(src => src.PropertyType.HasValue))
                .ForMember(dest => dest.SaleCategory, opt => opt.Condition(src => src.SaleCategory.HasValue))
                .ForMember(dest => dest.ListCaseStatus, opt => opt.Condition(src => src.ListCaseStatus.HasValue));

        }
    }
}
 

