using BuildingBlock.Domain.Exceptions;
using People.Domain.ContactAggregate.Entities;

namespace People.Domain.ContactAggregate.Exceptions;

public class ContactNotFoundException : EntityNotFoundException
{
    public ContactNotFoundException(Guid id) : base(nameof(Contact), id)
    {
    }
}