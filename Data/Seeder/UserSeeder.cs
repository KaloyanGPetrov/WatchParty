using Microsoft.AspNetCore.Identity;

namespace WatchParty.Data.Seeder
{
    public static class UserSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Admin", "user" };

            foreach (var role in roleNames)
            {
                bool roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }


            var adminUser = new IdentityUser
            {
                UserName = "Admin@admin.com",
                Email = "Admin@admin.com",
                EmailConfirmed = true
            };

            string password = "Admin#123";

            var user = await userManager.FindByEmailAsync(adminUser.Email);

            if (user == null)
            {
                var created = await userManager.CreateAsync(adminUser, password);
                if (created.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
