using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Infrastructure.EFCore;
using BuildingBlock.Infrastructure.EFCore.Repositories;
using People.Domain.Entities;
using People.Infrastructure.EFCore;

namespace People.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IReadOnlyRepository<Account>, ReadOnlyRepository<PeopleDbContext, Account>>();
        services.AddScoped<IOperationRepository<Account>, OperationRepository<PeopleDbContext, Account>>();
        
        services.AddScoped<Func<BaseDbContext>>(provider => () => provider.GetService<PeopleDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork<PeopleDbContext>>();

        return services;
    }
}