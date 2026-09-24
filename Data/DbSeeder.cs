using Gift_of_Givers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gift_of_Givers.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            // Make sure the database and all migrations exist before seeding
            await context.Database.MigrateAsync();

            // 1. Admin role
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            // 2. Admin user
            var adminEmail = "admin@giftofgivers.local";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(admin, "Admin123!");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            // 3. Dummy areas of need
            if (!context.AreasOfNeed.Any())
            {
                context.AreasOfNeed.AddRange(
                    new AreaOfNeed { Name = "Flood Response", Region = "KwaZulu-Natal", XPercent = 62, YPercent = 78, Severity = "Critical", Notes = "Urgent shelter and clean water." },
                    new AreaOfNeed { Name = "Drought Relief", Region = "Northern Cape", XPercent = 30, YPercent = 72, Severity = "High", Notes = "Food parcels and boreholes." },
                    new AreaOfNeed { Name = "Displacement Support", Region = "Gauteng", XPercent = 52, YPercent = 45, Severity = "Medium", Notes = "Housing and medical support." },
                    new AreaOfNeed { Name = "Wildfire Recovery", Region = "Western Cape", XPercent = 18, YPercent = 82, Severity = "High", Notes = "Rebuilding homes." }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
