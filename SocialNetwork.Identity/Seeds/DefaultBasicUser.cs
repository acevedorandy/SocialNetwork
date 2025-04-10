using Microsoft.AspNetCore.Identity;
using SocialNetwork.Application.Enum.IdentityLayer;
using SocialNetwork.Identity.Shared.Entities;

namespace SocialNetwork.Identity.Seeds
{
    public class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser applicationUser = new();

            applicationUser.UserName = "basicuser";
            applicationUser.Nombre = "Basic";
            applicationUser.Apellido = "User";
            applicationUser.Email = "basicuser@gmail.com";
            applicationUser.EmailConfirmed = true;
            applicationUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != applicationUser.Id))
            {
                var user = await userManager.FindByEmailAsync(applicationUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(applicationUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(applicationUser, RolesEnum.Basic.ToString());
                }
            }
        }
    }
}
