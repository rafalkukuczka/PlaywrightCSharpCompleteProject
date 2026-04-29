using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SampleMvcApp.Data;

namespace PlaywrightCSharpTests.TestData
{
    public static class TestDataScenarios
    {
        public static async Task EmptyDatabaseAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await ResetAsync(scope.ServiceProvider);
        }

        public static async Task DefaultSeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            await ResetAsync(scope.ServiceProvider);
            await DatabaseSeeder.SeedAsync(scope.ServiceProvider);
        }

        public static async Task RolesOnlyAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            await ResetAsync(scope.ServiceProvider);

            //Seed Roles only
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }

        public static async Task AdminUserAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            await ResetAsync(scope.ServiceProvider);
            await DatabaseSeeder.SeedAsync(scope.ServiceProvider);
        }

        private static async Task ResetAsync(IServiceProvider services)
        {
            var db = services.GetRequiredService<ApplicationDbContext>();

            var users = db.Users.ToList();
            db.Users.RemoveRange(users);

            var roles = db.Roles.ToList();
            db.Roles.RemoveRange(roles);

            await db.SaveChangesAsync();
        }

    }
}
