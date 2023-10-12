using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Domain.DealAggregate.Specifications;

public class DealLineProductNamePartialMatchSpecification : Specification<DealLine>
{
    private readonly string _name;

    public DealLineProductNamePartialMatchSpecification(string name)
    {
        _name = name;
    }

    public override Expression<Func<DealLine, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_name)) return dealLine => true;

        return dealLine => dealLine.Product.Name.ToUpper().Contains(_name.ToUpper());
    }
}