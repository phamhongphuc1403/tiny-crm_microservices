using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using People.Domain.ContactAggregate.Entities;

namespace People.Domain.ContactAggregate.Specifications;

public class ContactIdNotEqualSpecification : Specification<Contact>
{
    private readonly Guid _id;

    public ContactIdNotEqualSpecification(Guid id)
    {
        _id = id;
    }

    public override Expression<Func<Contact, bool>> ToExpression()
    {
        return contact => contact.Id != _id;
    }
}