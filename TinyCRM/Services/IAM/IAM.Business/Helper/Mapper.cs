using AutoMapper;
using IAM.Business.Models.Users.Dto;
using IAM.Domain.Entities.Users;

namespace IAM.Business.Helper;

public class Mapper:Profile
{
    public Mapper()
    {
        CreateMap<ApplicationUser,UserSummaryDto>();
    }
}