using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.Extensions;

public static class MapperExtension
{
    public static IServiceCollection AddMapper<TMapper>(this IServiceCollection services) where TMapper : Profile
    {
        services.AddAutoMapper(typeof(TMapper).GetTypeInfo().Assembly);

        return services;
    }
}