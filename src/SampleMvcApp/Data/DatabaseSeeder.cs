using Microsoft.AspNetCore.Identity;
using SampleMvcApp.Models;

namespace SampleMvcApp.Data;

public static class DatabaseSeeder
{
    public const string AdminEmail = "admin@company.com";
    public const string AdminPassword = "Password123!";

    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var db = services.GetRequiredService<ApplicationDbContext>();

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        var admin = await userManager.FindByEmailAsync(AdminEmail);
        if (admin is null)
        {
            admin = new ApplicationUser
            {
                UserName = AdminEmail,
                Email = AdminEmail,
                EmailConfirmed = true,
                DisplayName = "Test Admin"
            };

            var result = await userManager.CreateAsync(admin, AdminPassword);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            await userManager.AddToRoleAsync(admin, "Admin");
        }

        if (!db.Orders.Any())
        {
            db.Orders.AddRange(
                new Order { CustomerName = "ACME Manufacturing", OrderNumber = "ORD-SEED-001", Quantity = 10 },
                new Order { CustomerName = "Industrial Robotics GmbH", OrderNumber = "ORD-SEED-002", Quantity = 25 },
                new Order { CustomerName = "Factory Automation AG", OrderNumber = "ORD-SEED-003", Quantity = 50 }
            );

            await db.SaveChangesAsync();
        }
    }
}
