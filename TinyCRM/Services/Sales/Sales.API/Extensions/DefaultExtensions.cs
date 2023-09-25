using System.Reflection;
using System.Text.Json.Serialization;
using BuildingBlock.Presentation.Authentication;
using BuildingBlock.Presentation.Authorization;
using BuildingBlock.Presentation.Extensions;
using FluentValidation;
using Sales.Application;
using Sales.Infrastructure.EFCore;
using Sales.Infrastructure.EFCore.Mapper;

namespace Sales.API.Extensions;

public static class DefaultExtensions
{
    public static async Task<IServiceCollection> AddDefaultExtensions(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services
            .AddDefaultOpenApi(configuration)
            .AddCqrs<SalesApplicationAssemblyReference>()
            .AddEventBus(configuration)
            .AddAutoMapper(Assembly.GetAssembly(typeof(Mapper)))
            .AddDatabase<SaleDbContext>(configuration)
            .AddRedisCache(configuration)
            .AddDependencyInjection()
            .AddTinyCRMAuthentication(configuration)
            .AddValidatorsFromAssembly(typeof(SalesApplicationAssemblyReference).Assembly)
            .AddAuthorizations();

        await services.ApplyMigrationAsync<SaleDbContext>();

        return services;
    }
}