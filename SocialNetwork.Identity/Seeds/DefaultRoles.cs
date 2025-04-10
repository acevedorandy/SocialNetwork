

using Microsoft.AspNetCore.Identity;
using SocialNetwork.Application.Enum.IdentityLayer;
using SocialNetwork.Identity.Shared.Entities;

namespace SocialNetwork.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.Basic.ToString()));

        }

    }
}
