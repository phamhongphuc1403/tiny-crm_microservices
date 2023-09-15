using System.Linq.Expressions;
using BuildingBlock.Domain.Helper.Specification;
using Sales.Domain.Entities;

namespace Sales.Domain.Specifications;

public class LeadTitleSpecification : Specification<Lead>, ISpecification<Lead>
{
    private readonly string _keyword;

    public LeadTitleSpecification(string keyword)
    {
        _keyword = keyword;
    }

    public override Expression<Func<Lead, bool>> ToExpression()
    {
        return lead => lead.Title.Contains(_keyword);
    }
}