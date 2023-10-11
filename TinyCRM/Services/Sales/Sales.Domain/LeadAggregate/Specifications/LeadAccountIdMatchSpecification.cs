using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;

namespace Sales.Domain.LeadAggregate.Specifications;

public class LeadAccountIdMatchSpecification : Specification<Lead>
{
    private readonly Guid _accountId;

    public LeadAccountIdMatchSpecification(Guid accountId)
    {
        _accountId = accountId;
    }

    public override Expression<Func<Lead, bool>> ToExpression()
    {
        return lead => lead.CustomerId == _accountId;
    }
}