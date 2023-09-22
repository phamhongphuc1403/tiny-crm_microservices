using BuildingBlock.Domain.Exceptions;
using People.Domain.AccountAggregate.Entities;

namespace People.Domain.AccountAggregate.Exceptions;

public class AccountDuplicatedException : EntityDuplicatedException
{
    public AccountDuplicatedException(string column, string value) : base(nameof(Account), column, value)
    {
    }
}