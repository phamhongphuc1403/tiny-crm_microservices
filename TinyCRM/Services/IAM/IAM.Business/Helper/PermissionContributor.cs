using System.Security.Claims;
using IAM.Domain.Entities.Roles;
using Microsoft.AspNetCore.Identity;

namespace IAM.Business.Helper;

public class PermissionContributor
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public PermissionContributor(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedPermissionsAsync()
    {
        await SeedRolesAsync();
        var admin = await _roleManager.FindByNameAsync(Role.Admin);
        var user = await _roleManager.FindByNameAsync(Role.User);

        await SeedUserPermissionsAsync(user!);
        await SeedAdminPermissionsAsync(admin!);
    }

    private async Task SeedUserPermissionsAsync(ApplicationRole user)
    {
        var permissionsUsers = PermissionHandler.GetPermissionClaims()
            .Where(claim => claim.Value.Contains("Read")).ToList();
        var userClaimsExisting = (await _roleManager.GetClaimsAsync(user)).Any();

        if (userClaimsExisting)
        {
            var userClaims = await _roleManager.GetClaimsAsync(user);
            foreach (var claim in userClaims)
                if (!permissionsUsers.Contains(claim))
                    await _roleManager.RemoveClaimAsync(user, claim);
        }

        foreach (var claim in permissionsUsers)
            await _roleManager.AddClaimAsync(user, claim);
    }

    private async Task SeedAdminPermissionsAsync(ApplicationRole admin)
    {
        var valuePermissions = PermissionHandler.GetPermissionClaims()
            .Select(p => p.Value).ToList();
        var valuePermissionsSuperAdmin = (await _roleManager.GetClaimsAsync(admin))
            .Select(p => p.Value).ToList();

        var hashSetValuePermissions = new HashSet<string>(valuePermissions);
        var hashSetValuePermissionsExisting = new HashSet<string>(valuePermissionsSuperAdmin);

        var permissionsToAdd = hashSetValuePermissions.Except(hashSetValuePermissionsExisting);

        foreach (var permissionToAdd in permissionsToAdd)
            await _roleManager.AddClaimAsync(admin, new Claim("Permission", permissionToAdd));

        var permissionsToRemove = hashSetValuePermissionsExisting.Except(hashSetValuePermissions);

        foreach (var permissionToRemove in permissionsToRemove)
            await _roleManager.RemoveClaimAsync(admin, new Claim("Permission", permissionToRemove));
    }

    private async Task SeedRolesAsync()
    {
        var roles = new[] { Role.Admin, Role.User };

        foreach (var role in roles)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role);
            if (!roleExist) await _roleManager.CreateAsync(new ApplicationRole(role));
        }
    }
}