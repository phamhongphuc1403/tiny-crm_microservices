using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;

namespace Sales.Domain.AccountAggregate.Specifications;

public class AccountEmailExactMatchSpecification : Specification<Account>
{
    private readonly string? _email;

    public AccountEmailExactMatchSpecification(string? email)
    {
        _email = email;
    }

    public override Expression<Func<Account, bool>> ToExpression()
    {
        return account => account.Email != null && account.Email == _email;
    }
}