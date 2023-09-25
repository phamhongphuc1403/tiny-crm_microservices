using BuildingBlock.Domain.Exceptions;

namespace Sales.Domain.AccountAggregate.Exceptions;

public class AccountNotFoundException : EntityNotFoundException
{
    public AccountNotFoundException(Guid id) : base(nameof(Account), id)
    {
    }
}