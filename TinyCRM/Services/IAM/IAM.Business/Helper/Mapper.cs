using AutoMapper;
using IAM.Business.Models.Users.Dto;
using IAM.Domain.Entities.Users;

namespace IAM.Business.Helper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<ApplicationUser, UserSummaryDto>();
        CreateMap<ApplicationUser, UserDetailDto>();
        CreateMap<UserCreateDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        ;
        CreateMap<UserEditDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        ;
    }
}