using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;

namespace Sales.Domain.AccountAggregate.Specifications;

public class AccountNamePartialMatchSpecification : Specification<Account>
{
    private readonly string _name;

    public AccountNamePartialMatchSpecification(string name)
    {
        _name = name;
    }

    public override Expression<Func<Account, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_name)) return account => true;

        return account => account.Name.ToUpper().Contains(_name.ToUpper());
    }
}