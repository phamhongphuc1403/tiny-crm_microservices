using BuildingBlock.Application;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Infrastructure.EFCore;
using BuildingBlock.Infrastructure.EFCore.Repositories;
using Sales.Application.Seeds;
using Sales.Domain.AccountAggregate;
using Sales.Domain.AccountAggregate.DomainService;
using Sales.Domain.DealAggregate.DomainService;
using Sales.Domain.DealAggregate.Entities;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.DomainService;
using Sales.Domain.ProductAggregate.DomainService;
using Sales.Domain.ProductAggregate.Entities;
using Sales.Domain.ProductAggregate.Repositories;
using Sales.Infrastructure.EFCore;
using Sales.Infrastructure.EFCore.Repositories.ProductRepositories;

namespace Sales.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IReadOnlyRepository<Lead>, ReadOnlyRepository<SaleDbContext, Lead>>();
        services.AddScoped<IReadOnlyRepository<Account>, ReadOnlyRepository<SaleDbContext, Account>>();
        services.AddScoped<IReadOnlyRepository<Product>, ReadOnlyRepository<SaleDbContext, Product>>();
        services.AddScoped<IReadOnlyRepository<Deal>, ReadOnlyRepository<SaleDbContext, Deal>>();

        services.AddScoped<IOperationRepository<Lead>, OperationRepository<SaleDbContext, Lead>>();
        services.AddScoped<IOperationRepository<Account>, OperationRepository<SaleDbContext, Account>>();
        services.AddScoped<IOperationRepository<Product>, OperationRepository<SaleDbContext, Product>>();
        services.AddScoped<IProductOperationRepository, ProductOperationRepository>();
        services.AddScoped<IOperationRepository<Deal>, OperationRepository<SaleDbContext, Deal>>();
        services.AddScoped<IReadOnlyRepository<DealLine>, ReadOnlyRepository<SaleDbContext, DealLine>>();

        services.AddScoped<IAccountDomainService, AccountDomainService>();
        services.AddScoped<ILeadDomainService, LeadDomainService>();
        services.AddScoped<IProductDomainService, ProductDomainService>();
        services.AddScoped<IDealDomainService, DealDomainService>();

        services.AddScoped<Func<BaseDbContext>>(provider => () => provider.GetService<SaleDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork<SaleDbContext>>();

        services.AddScoped<IDataSeeder, ProductSeeder>();
        services.AddScoped<IDataSeeder, LeadSeeder>();
        return services;
    }
}