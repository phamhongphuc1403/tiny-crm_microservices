using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using People.Domain.Entities;

namespace People.Domain.Specifications;

public class AccountEmailPartialMatchSpecification : Specification<Account>
{
    private readonly string _email;

    public AccountEmailPartialMatchSpecification(string email)
    {
        _email = email;
    }

    public override Expression<Func<Account, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_email)) return account => true;

        return account => account.Email.Contains(_email);
    }
}