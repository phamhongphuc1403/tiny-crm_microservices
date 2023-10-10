using Sales.Application.DTOs.AccountDTOs;
using Sales.Domain.AccountAggregate;

namespace Sales.Infrastructure.EFCore.Mapper;

public class AccountMapper : Mapper
{
    public AccountMapper()
    {
        CreateMap<Account, AccountSummaryDto>();
    }
}