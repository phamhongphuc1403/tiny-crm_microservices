using AutoMapper;
using People.Application.DTOs;
using People.Domain.Entities;

namespace People.Infrastructure.EFCore;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Account, AccountSummaryDto>();
        CreateMap<Account, AccountDetailDto>();
    }
}