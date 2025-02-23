using System;
using EmployeePortal.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeePortal.Data.Seed;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<Employee> userManager, RoleManager<Role> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new Role { Name = "Admin" });
        }
        if (!await roleManager.RoleExistsAsync("Manager"))
        {
            await roleManager.CreateAsync(new Role { Name = "Manager" });
        }
        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new Role { Name = "User" });
        }

        var user = await userManager.FindByEmailAsync("admin@admin.com");
        if (user == null)
        {
            user = new Employee
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                PhoneNumber = "123-456-7890",
                Salary = "50000"
            };
            await userManager.CreateAsync(user, "Password123!");
            await userManager.AddToRoleAsync(user, "Admin");
        }   
    }
}
