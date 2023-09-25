using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using People.Domain.ContactAggregate.Entities;

namespace People.Domain.ContactAggregate.Specifications;

public class ContactEmailPartialMatchSpecification : Specification<Contact>
{
    private readonly string _email;

    public ContactEmailPartialMatchSpecification(string email)
    {
        _email = email;
    }

    public override Expression<Func<Contact, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_email)) return contact => true;

        return contact => contact.Email != null && contact.Email.ToUpper().Contains(_email.ToUpper());
    }
}