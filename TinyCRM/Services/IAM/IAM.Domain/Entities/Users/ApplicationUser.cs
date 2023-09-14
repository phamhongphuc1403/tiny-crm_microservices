using Microsoft.AspNetCore.Identity;

namespace IAM.Domain.Entities.Users;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = null!;
}