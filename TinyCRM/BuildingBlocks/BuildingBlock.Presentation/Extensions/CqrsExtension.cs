using BuildingBlock.Application.PipelineBehaviors;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.Extensions;

public static class CqrsExtension
{
    public static IServiceCollection AddCqrs<TApplicationAssemblyReference>(this IServiceCollection services)
        where TApplicationAssemblyReference : class
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(TApplicationAssemblyReference));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}