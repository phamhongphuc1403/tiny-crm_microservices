using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using People.Domain.Entities;

namespace People.Domain.Specifications;

public class AccountIdNotEqualSpecification : Specification<Account>
{
    private readonly Guid _id;

    public AccountIdNotEqualSpecification(Guid id)
    {
        _id = id;
    }

    public override Expression<Func<Account, bool>> ToExpression()
    {
        return account => account.Id != _id;
    }
}