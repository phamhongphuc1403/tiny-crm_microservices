using BuildingBlock.Domain.Exceptions;
using People.Domain.Entities;

namespace People.Domain.Exceptions;

public class AccountDuplicatedException : EntityDuplicatedException
{
    public AccountDuplicatedException(string column, string value) : base(nameof(Account), column, value)
    {
    }
}