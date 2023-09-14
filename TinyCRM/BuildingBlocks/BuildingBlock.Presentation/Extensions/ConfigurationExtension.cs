using Microsoft.Extensions.Configuration;

namespace BuildingBlock.Presentation.Extensions;

public static class ConfigurationExtension
{
    public static string GetRequiredValue(this IConfiguration configuration, string name)
    {
        return configuration[name] ?? throw new InvalidOperationException($"Configuration missing value for: {(configuration is IConfigurationSection s ? s.Path + ":" + name : name)}");
    }

    public static string GetRequiredConnectionString(this IConfiguration configuration, string name)
    {
        return configuration.GetConnectionString(name) ?? throw new InvalidOperationException($"Configuration missing value for: {(configuration is IConfigurationSection s ? s.Path + ":ConnectionStrings:" + name : "ConnectionStrings:" + name)}");

    }
}