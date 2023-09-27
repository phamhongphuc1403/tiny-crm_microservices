using BuildingBlock.Domain.Exceptions;
using People.Domain.ContactAggregate.Entities;

namespace People.Domain.ContactAggregate.Exceptions;

public class ContactDuplicatedException : EntityDuplicatedException
{
    public ContactDuplicatedException(string column, object value) : base(nameof(Contact), column, value)
    {
    }
}