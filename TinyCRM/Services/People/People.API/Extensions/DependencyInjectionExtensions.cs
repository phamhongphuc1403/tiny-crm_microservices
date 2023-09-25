using BuildingBlock.Application;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Infrastructure.EFCore;
using BuildingBlock.Infrastructure.EFCore.Repositories;
using People.Application.Seeders;
using People.Domain.AccountAggregate.Entities;
using People.Domain.ContactAggregate.Entities;
using People.Infrastructure.EFCore;

namespace People.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IReadOnlyRepository<Account>, ReadOnlyRepository<PeopleDbContext, Account>>();
        services.AddScoped<IOperationRepository<Account>, OperationRepository<PeopleDbContext, Account>>();
        services.AddScoped<IReadOnlyRepository<Contact>, ReadOnlyRepository<PeopleDbContext, Contact>>();
        services.AddScoped<IOperationRepository<Contact>, OperationRepository<PeopleDbContext, Contact>>();

        services.AddScoped<Func<BaseDbContext>>(provider => () => provider.GetService<PeopleDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork<PeopleDbContext>>();

        services.AddScoped<IDataSeeder, AccountSeeder>();
        services.AddScoped<IDataSeeder, ContactSeeder>();
        return services;
    }
}