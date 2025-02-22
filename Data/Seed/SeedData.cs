using System;
using Microsoft.AspNetCore.Identity;

namespace EmployeePortal.Data.Seed;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }
        if (!await roleManager.RoleExistsAsync("HR"))
        {
            await roleManager.CreateAsync(new IdentityRole("HR"));
        }
        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }
        var user = await userManager.FindByEmailAsync("admin@admin.com");
        if (user == null)
        {
            user = new IdentityUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com"
            };
            await userManager.CreateAsync(user, "Password123!");
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
