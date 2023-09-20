using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using People.Domain.Entities;

namespace People.Domain.Specifications;

public class AccountEmailExactMatchSpecification : Specification<Account>
{
    private readonly string _email;

    public AccountEmailExactMatchSpecification(string email)
    {
        _email = email;
    }

    public override Expression<Func<Account, bool>> ToExpression()
    {
        return account => account.Email.ToUpper() == _email.ToUpper();
    }
}