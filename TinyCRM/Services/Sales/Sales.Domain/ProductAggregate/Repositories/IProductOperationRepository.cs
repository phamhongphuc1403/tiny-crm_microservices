using BuildingBlock.Domain.Repositories;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Domain.ProductAggregate.Repositories;

public interface IProductOperationRepository : IOperationRepository<Product>
{
    void SoftRemoveRange(IEnumerable<Product> products);
}