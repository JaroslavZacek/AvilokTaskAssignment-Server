using Microsoft.AspNetCore.Identity;
using AvilokTaskAssignment.Data.Models;

namespace AvilokTaskAssignment.Api.Seed
{
    public class IdentitySeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
        }

        private static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            string[] roles =
                {
                    "Admin",
                    "Leader Developer",
                    "Developer",
                    "Leader Graphic",
                    "Graphic",
                    "Leader Story",
                    "Story"
                };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole 
                    { 
                        Name = role 
                    });
                }
            }
        }


        private static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            const string adminEmail = "jaroslav.zacek1991@gmail.com";
            const string adminPassword = "Zkouska.123";

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin != null)
                return;

            var adminUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Jaroslav Žáček",
                EmailConfirmed = false
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (!result.Succeeded)
            {
                throw new Exception("Nepodařilo se vytvořit Admina: " + 
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
