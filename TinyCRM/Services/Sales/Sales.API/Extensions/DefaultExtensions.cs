using System.Text.Json.Serialization;
using BuildingBlock.Presentation.Authentication;
using BuildingBlock.Presentation.Authorization;
using BuildingBlock.Presentation.Extensions;
using Sales.Application;
using Sales.Infrastructure.EFCore;

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
            .AddMapper<Mapper>()
            .AddDatabase<SaleDbContext>(configuration)
            .AddDependencyInjection()
            .AddTinyCRMAuthentication(configuration)
            .AddAuthorizations();

        await services.ApplyMigrationAsync<SaleDbContext>();

        return services;
    }
}