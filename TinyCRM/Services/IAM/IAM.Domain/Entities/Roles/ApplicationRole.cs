using Microsoft.AspNetCore.Identity;

namespace IAM.Domain.Entities.Roles;

public class ApplicationRole : IdentityRole
{
    public ApplicationRole()
    {
    }

    public ApplicationRole(string role) : base(role)
    {
    }

    public ICollection<IdentityRoleClaim<string>>? Claims { get; set; }
}