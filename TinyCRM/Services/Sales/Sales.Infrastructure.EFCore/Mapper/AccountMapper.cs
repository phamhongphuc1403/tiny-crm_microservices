using Sales.Application.DTOs.Accounts;
using Sales.Domain.AccountAggregate;

namespace Sales.Infrastructure.EFCore.Mapper;

public class AccountMapper:Mapper
{
    public AccountMapper()
    {
        CreateMap<Account, AccountResultDto>();
    }
}