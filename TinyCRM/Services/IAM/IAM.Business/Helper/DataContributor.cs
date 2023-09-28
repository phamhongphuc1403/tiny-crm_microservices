using Bogus;
using BuildingBlock.Application;
using IAM.Domain.Entities.Roles;
using IAM.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace IAM.Business.Helper;

public class DataContributor : IDataSeeder
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public DataContributor(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedDataAsync()
    {
        if (!_roleManager.Roles.Any() && !_userManager.Users.Any())
        {
            await _roleManager.CreateAsync(new ApplicationRole(Role.Admin));
            // await _roleManager.CreateAsync(new ApplicationRole(Role.User));

            var user = new ApplicationUser
            {
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                Name = "SuperAdmin"
            };

            await _userManager.CreateAsync(user, "@Admin123");
            await _userManager.AddToRoleAsync(user, Role.Admin);
            var faker = new Faker<ApplicationUser>()
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.UserName))
                .RuleFor(u => u.Name, f => f.Name.FullName());

            var users = faker.Generate(50);

            foreach (var applicationUser in users)
            {
                await _userManager.CreateAsync(applicationUser, "@Admin123");
                await _userManager.AddToRoleAsync(applicationUser, Role.Admin);
                // await _userManager.CreateAsync(applicationUser, "@User123");
                // await _userManager.AddToRoleAsync(applicationUser, Role.User);
            }
        }
    }
}