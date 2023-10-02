using BuildingBlock.Application;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Infrastructure.EFCore;
using BuildingBlock.Infrastructure.EFCore.Repositories;
using Sales.Application.Seeds;
using Sales.Domain.AccountAggregate;
using Sales.Domain.AccountAggregate.DomainService;
using Sales.Domain.LeadAggregate;
using Sales.Domain.ProductAggregate.Entities;
using Sales.Infrastructure.EFCore;

namespace Sales.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IReadOnlyRepository<Lead>, ReadOnlyRepository<SaleDbContext, Lead>>();
        services.AddScoped<IReadOnlyRepository<Account>, ReadOnlyRepository<SaleDbContext, Account>>();
        services.AddScoped<IOperationRepository<Account>, OperationRepository<SaleDbContext, Account>>();
        services.AddScoped<IReadOnlyRepository<Product>, ReadOnlyRepository<SaleDbContext, Product>>();
        services.AddScoped<IOperationRepository<Product>, OperationRepository<SaleDbContext, Product>>();
        services.AddScoped<IAccountDomainService, AccountDomainService>();

        services.AddScoped<Func<BaseDbContext>>(provider => () => provider.GetService<SaleDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork<SaleDbContext>>();

        services.AddScoped<IDataSeeder, ProductSeeder>();
        return services;
    }
}