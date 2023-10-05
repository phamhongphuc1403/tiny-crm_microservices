using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Domain.ProductAggregate.Specifications;

public class ProductDeletedSpecification : Specification<Product>
{
    private readonly bool _showDeleted;

    public ProductDeletedSpecification(bool showDeleted)
    {
        _showDeleted = showDeleted;
    }

    public override Expression<Func<Product, bool>> ToExpression()
    {
        if (_showDeleted) return product => true;

        return product => product.DeletedDate == null;
    }
}