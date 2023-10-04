using Sales.Domain.ProductAggregate.Entities;
using Sales.Domain.ProductAggregate.Entities.Enums;

namespace Sales.Domain.ProductAggregate.DomainService;

public interface IProductDomainService
{
    Task<Product> CreateAsync(string code, string name, double price, bool isAvailable, ProductType type);
    Task<Product> EditAsync(Guid id, string code, string name, double price, bool isAvailable, ProductType type);
}