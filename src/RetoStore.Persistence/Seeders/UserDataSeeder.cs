﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RetoStore.Entities;

namespace RetoStore.Persistence.Seeders;

public class UserDataSeeder
{
    private readonly IServiceProvider service;

    public UserDataSeeder(IServiceProvider service)
    {
        this.service = service;
    }

    public async Task SeedAsync()
    {
        //User repository
        var userManager = service.GetRequiredService<UserManager<RetoStoreUserIdentity>>();
        //Role repository
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
        //Creating roles
        var adminRole = new IdentityRole(Constants.RoleAdmin);
        var customerRole = new IdentityRole(Constants.RoleCustomer);

        if (!await roleManager.RoleExistsAsync(Constants.RoleAdmin))
        {
            await roleManager.CreateAsync(adminRole);
        }

        if (!await roleManager.RoleExistsAsync(Constants.RoleCustomer))
        {
            await roleManager.CreateAsync(customerRole);
        }

        //Admin user
        var adminUser = new RetoStoreUserIdentity
        {
            FirstName = "System",
            LastName = "Administrator",
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            PhoneNumber = "123456789",
            Age = 35,
            DocumentType = DocumentTypeEnum.Dni,
            DocumentNumber = "12345678",
            EmailConfirmed = true
        };
        if (await userManager.FindByEmailAsync(adminUser.UserName) == null)
        {
            var result = await userManager.CreateAsync(adminUser, "Admin1234*");
            if (result.Succeeded)
            {
                adminUser = await userManager.FindByEmailAsync(adminUser.Email);
                if (adminUser is not null)
                    await userManager.AddToRoleAsync(adminUser, Constants.RoleAdmin);
            }
        }
    }
}
