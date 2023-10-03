using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Domain.ProductAggregate.Specifications;

public class ProductIdSpecification : Specification<Product>
{
    private readonly Guid _id;

    public ProductIdSpecification(Guid id)
    {
        _id = id;
    }

    public override Expression<Func<Product, bool>> ToExpression()
    {
        return product => product.Id == _id;
    }
}