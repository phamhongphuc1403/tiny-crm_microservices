using System.Text.Json.Serialization;
using BuildingBlock.Application.Identity;
using BuildingBlock.Presentation.Authentication;
using BuildingBlock.Presentation.Authorization;
using BuildingBlock.Presentation.Extensions;
using FluentValidation;
using People.Application;
using People.Infrastructure.EFCore;
using People.Infrastructure.EFCore.Profiles;

namespace People.API.Extensions;

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
            .AddCqrs<PeopleApplicationAssemblyReference>()
            .AddMapper<Mapper>()
            .AddDatabase<PeopleDbContext>(configuration)
            .AddRedisCache(configuration)
            .AddDependencyInjection()
            .AddTinyCRMAuthentication(configuration)
            .AddValidatorsFromAssembly(typeof(PeopleApplicationAssemblyReference).Assembly)
            .AddAuthorizations()
            .AddMailService(configuration)
            .AddEventBus(configuration)
            ;

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();

        await services.ApplyMigrationAsync<PeopleDbContext>();

        return services;
    }
}