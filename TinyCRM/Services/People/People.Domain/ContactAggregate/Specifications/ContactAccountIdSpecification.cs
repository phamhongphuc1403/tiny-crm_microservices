using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using People.Domain.ContactAggregate.Entities;

namespace People.Domain.ContactAggregate.Specifications;

public class ContactAccountIdSpecification : Specification<Contact>
{
    private readonly Guid _accountId;

    public ContactAccountIdSpecification(Guid accountId)
    {
        _accountId = accountId;
    }

    public override Expression<Func<Contact, bool>> ToExpression()
    {
        return contact => contact.AccountId == _accountId;
    }
}