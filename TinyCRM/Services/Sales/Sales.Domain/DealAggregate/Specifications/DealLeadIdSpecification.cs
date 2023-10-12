using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Domain.DealAggregate.Specifications;

public class DealLeadIdSpecification:Specification<Deal>
{
    private readonly Guid _leadId;

    public DealLeadIdSpecification(Guid leadId)
    {
        _leadId = leadId;
    }

    public override Expression<Func<Deal, bool>> ToExpression()
    {
        return deal => deal.LeadId == _leadId;
    }
}