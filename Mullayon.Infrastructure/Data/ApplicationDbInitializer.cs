using Microsoft.AspNetCore.Identity;
using Mullayon.Core.Constants;
using Mullayon.Core.Entities;

namespace Mullayon.Infrastructure.Data;

public static class ApplicationDbInitializer
{
    public static void SeedUsers(UserManager<ApplicationUser> userManager)
    {
        if (userManager.FindByEmailAsync("admin@mullayon.com").Result == null)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@mullayon.com"
            };

            IdentityResult result = userManager.CreateAsync(user, "Admin@123").Result;
            
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, RoleStrings.Admin).Wait();
            }
        }
    }
}