using AutoMapper;
using People.Application.DTOs.AccountDTOs;
using People.Domain.AccountAggregate.Entities;

namespace People.Infrastructure.EFCore.Profiles;

public class AccountMapper : Profile
{
    public AccountMapper()
    {
        CreateMap<Account, AccountSummaryDto>();
        CreateMap<Account, AccountDetailDto>();
    }
}