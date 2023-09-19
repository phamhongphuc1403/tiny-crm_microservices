using System.Reflection;
using System.Security.Claims;
using BuildingBlock.Application.Identity.Permissions;

namespace IAM.Business.Helper;

public static class PermissionHandler
{
    public static IEnumerable<string?> GetPermissionNames()
    {
        var permissionNames = typeof(TinyCrmPermissions).GetNestedTypes(BindingFlags.Public)
            .SelectMany(nestedType => nestedType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(field =>
                    field.FieldType == typeof(string) && field is { IsLiteral: true, IsInitOnly: false })
                .Select(field => field.GetValue(null) as string))
            .ToList();

        return permissionNames;
    }

    public static Claim ConvertToClaim(string? permissionName)
    {
        return new Claim("Permission", permissionName ?? "");
    }

    public static List<Claim> GetPermissionClaims()
    {
        var permissionNames = GetPermissionNames();
        var claims = permissionNames.Select(ConvertToClaim).ToList();
        return claims;
    }

    public static IEnumerable<string?> GetInvalidPermissions(ICollection<string>? permissions)
    {
        if (permissions is null)
            return new List<string?>();
        var permissionNames = GetPermissionNames();
        var invalidPermissions = permissions.Except(permissionNames);
        return invalidPermissions;
    }

    public static List<string?> ListInvalidPermissions(IEnumerable<string>? permissions)
    {
        var invalidPermissions = GetInvalidPermissions(permissions as ICollection<string>).ToList();
        return invalidPermissions;
    }
}