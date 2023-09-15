using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Infrastructure.EFCore;
using Sales.Domain.Repositories.LeadRepository;
using Sales.Infrastructure.EFCore;
using Sales.Infrastructure.EFCore.Repositories;

namespace Sales.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ILeadReadOnlyRepository, LeadReadOnlyRepository>();

        services.AddScoped<Func<BaseDbContext>>(provider => () => provider.GetService<SaleDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork<SaleDbContext>>();

        return services;
    }
}