using BuildingBlock.Domain.Exceptions;

namespace Sales.Domain.AccountAggregate.Exceptions;

public class AccountDuplicatedException : EntityDuplicatedException
{
    public AccountDuplicatedException(string column, string value) : base(nameof(Account), column, value)
    {
    }
}