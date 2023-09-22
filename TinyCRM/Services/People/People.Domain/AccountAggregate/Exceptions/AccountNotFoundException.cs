using BuildingBlock.Domain.Exceptions;
using People.Domain.Entities;

namespace People.Domain.Exceptions;

public class AccountNotFoundException : EntityNotFoundException
{
    public AccountNotFoundException(Guid id) : base(nameof(Account), id)
    {
    }
}