using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingProject.Data;
using ShoppingProject.Data.Models;

namespace ShoppingProject.Infrastructure
{
    public static class DataSeeder
    {
        public static async Task Initialize(IServiceProvider app)
        {
            var roleManager = app.GetRequiredService<RoleManager<IdentityRole<int>>>();
            await EnsureRole(roleManager);

            var userManager = app.GetRequiredService<UserManager<User>>();
            await EnsureAdmin(userManager);
        }
        
        public static async Task EnsureRole(RoleManager<IdentityRole<int>> roleManager)
        {
            var exists = await roleManager.RoleExistsAsync(DataConstants.Role.Administrator);

            if (exists)
            {
                return;
            }

            await roleManager.CreateAsync(new IdentityRole<int>(DataConstants.Role.Administrator));

        }

        public static async Task EnsureAdmin(UserManager<User> userManager)
        {
            var admin = await userManager.Users.Where(u => u.UserName == "boyanthedude@admin.com")
                .SingleOrDefaultAsync();

            if (admin == null)
            {
                return;
            }

            admin = new User
            {
                UserName = "boyanthedude@admin.com",
                Email = "boyanthedude@admin.com"
            };

            await userManager.CreateAsync(
            admin, "admin123");
            await userManager.AddToRoleAsync(admin, DataConstants.Role.Administrator);
        }

        public static void DatabaseInitializer(WebApplication app)
        {
            using(var scoped = app.Services.CreateScope())
            {
                var services = scoped.ServiceProvider;

                try
                {
                    DataSeeder.Initialize(services).Wait();
            }
                catch (Exception exception)
                {
                    ILogger<Program>? logger = services.GetService<ILogger<Program>>();

                    logger.LogError(exception, "Error occured !!");
                }
            }

            
        }
    }
}
