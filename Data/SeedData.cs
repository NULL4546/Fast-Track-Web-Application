using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Fast_Track_Web_Application.Data
{
    public static class SeedData
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Create roles
            string[] roles = { "Admin", "SuperAdmin" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create pre-made Super Admin accounts
            var superAdmins = new List<(string Email, string Password)>
            {
                ("superadmin1@example.com", "SuperAdmin#123"),
                ("superadmin2@example.com", "SuperAdmin#123"),
                ("superadmin3@example.com", "SuperAdmin#123")
            };

            foreach (var (email, password) in superAdmins)
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new IdentityUser { UserName = email, Email = email };
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "SuperAdmin");
                    }
                }
            }



            // Log information about the seeding process using ILogger<Program>
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>(); // Use ILogger<Program> instead of ILogger<SeedData>
            logger.LogInformation("Roles and Super Admin accounts have been seeded.");
        }
    }
}
