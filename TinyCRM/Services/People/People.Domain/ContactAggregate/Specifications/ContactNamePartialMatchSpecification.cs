using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using People.Domain.ContactAggregate.Entities;

namespace People.Domain.ContactAggregate.Specifications;

public class ContactNamePartialMatchSpecification : Specification<Contact>
{
    private readonly string _name;

    public ContactNamePartialMatchSpecification(string name)
    {
        _name = name;
    }

    public override Expression<Func<Contact, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_name)) return contact => true;

        return contact => contact.Name.ToUpper().Contains(_name.ToUpper());
    }
}