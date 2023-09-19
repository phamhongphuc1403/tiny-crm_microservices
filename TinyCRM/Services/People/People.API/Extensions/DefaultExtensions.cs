using System.Text.Json.Serialization;
using AutoMapper;
using BuildingBlock.Application.Identity;
using BuildingBlock.Presentation.Authentication;
using BuildingBlock.Presentation.Authorization;
using BuildingBlock.Presentation.Extensions;
using People.Application;
using People.Infrastructure.EFCore;
using Mapper = People.Infrastructure.EFCore.Mapper;

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
            .AddDependencyInjection()
            .AddTinyCRMAuthentication(configuration)
            .AddAuthorizations();

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();
        
        await services.ApplyMigrationAsync<PeopleDbContext>();

        return services;
    }
}