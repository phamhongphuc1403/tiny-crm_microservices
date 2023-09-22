using BuildingBlock.Domain.Exceptions;
using People.Domain.AccountAggregate.Entities;

namespace People.Domain.AccountAggregate.Exceptions;

public class AccountNotFoundException : EntityNotFoundException
{
    public AccountNotFoundException(Guid id) : base(nameof(Account), id)
    {
    }
}