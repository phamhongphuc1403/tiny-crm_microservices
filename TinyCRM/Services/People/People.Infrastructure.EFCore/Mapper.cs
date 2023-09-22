using AutoMapper;
using People.Application.DTOs.AccountDTOs;
using People.Domain.AccountAggregate.Entities;

namespace People.Infrastructure.EFCore;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Account, AccountSummaryDto>();
        CreateMap<Account, AccountDetailDto>();
    }
}