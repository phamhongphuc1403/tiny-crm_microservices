using BuildingBlock.Infrastructure.EFCore.Repositories;
using Sales.Domain.ProductAggregate.Entities;
using Sales.Domain.ProductAggregate.Repositories;

namespace Sales.Infrastructure.EFCore.Repositories.ProductRepositories;

public class ProductOperationRepository : OperationRepository<SaleDbContext, Product>, IProductOperationRepository
{
    public ProductOperationRepository(SaleDbContext dbContext) : base(dbContext)
    {
    }

    public void SoftRemoveRange(IEnumerable<Product> products)
    {
        foreach (var product in products)
        {
            product.DeletedDate = DateTime.UtcNow;

            Update(product);
        }
    }
}