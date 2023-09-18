using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using People.Domain.Entities;

namespace People.Domain.Specifications;

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
        
        return account => account.Name.Contains(_name);
    }
}