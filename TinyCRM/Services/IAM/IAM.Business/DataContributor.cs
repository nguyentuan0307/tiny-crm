﻿using Bogus;
using IAM.Domain.Entities.Roles;
using IAM.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace IAM.Business;

public class DataContributor
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public DataContributor(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        if (!_roleManager.Roles.Any() && !_userManager.Users.Any())
        {
            await _roleManager.CreateAsync(new ApplicationRole(Role.SuperAdmin));
            await _roleManager.CreateAsync(new ApplicationRole(Role.Admin));
            await _roleManager.CreateAsync(new ApplicationRole(Role.User));

            var user = new ApplicationUser
            {
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                Name = "superAdmin"
            };

            await _userManager.CreateAsync(user, "@SuperAdmin123");
            await _userManager.AddToRoleAsync(user, Role.SuperAdmin);
            var faker = new Faker<ApplicationUser>()
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.UserName))
                .RuleFor(u => u.Name, f => f.Name.FullName());

            var users = faker.Generate(10);

            foreach (var applicationUser in users)
            {
                await _userManager.CreateAsync(applicationUser, "@User123");
                await _userManager.AddToRoleAsync(applicationUser, Role.User);
            }
        }
    }
}