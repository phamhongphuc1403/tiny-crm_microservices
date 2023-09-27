using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using People.Domain.ContactAggregate.Entities;

namespace People.Domain.ContactAggregate.Specifications;

public class ContactEmailExactMatchSpecification : Specification<Contact>
{
    private readonly string _email;

    public ContactEmailExactMatchSpecification(string email)
    {
        _email = email;
    }

    public override Expression<Func<Contact, bool>> ToExpression()
    {
        return contact => contact.Email != null && contact.Email.ToUpper() == _email.ToUpper();
    }
}