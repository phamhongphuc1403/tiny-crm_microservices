using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.Entities;
using Sales.Domain.Entities.Enums;

namespace Sales.Domain.Specifications;

public class LeadStatusSpecification : Specification<Lead>, ISpecification<Lead>
{
    private readonly LeadStatus? _status;

    public LeadStatusSpecification(LeadStatus? status)
    {
        _status = status;
    }

    public override Expression<Func<Lead, bool>> ToExpression()
    {
        return _status != null ? lead => lead.Status == _status : lead => true;
    }
}