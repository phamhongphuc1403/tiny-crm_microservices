using IAM.Business.Helper;
using IAM.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace IAM.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var seedData = scope.ServiceProvider.GetRequiredService<DataContributor>();
        var seedPermissions = scope.ServiceProvider.GetRequiredService<PermissionContributor>();
        await seedData.SeedAsync();
        await seedPermissions.SeedPermissionsAsync();
    }

    public static async Task ApplyMigrationAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<IdentityDataContext>();
        if ((await context.Database.GetPendingMigrationsAsync()).Any()) await context.Database.MigrateAsync();
    }
}