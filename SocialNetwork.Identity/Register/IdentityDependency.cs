using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Identity.Shared.Context;
using SocialNetwork.Identity.Shared.Entities;
using SocialNetwork.Identity.Helper;
using SocialNetwork.Application.Contracts.identity;
using SocialNetwork.Identity.Seeds;
using SocialNetwork.Identity.Services;

namespace SocialNetwork.Identity.Register
{
    public static class IdentityDependency
    {
        public static void AddIdentityLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdendityDb"));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                        m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });
            }
            #endregion

            #region Identity

            services.AddIdentityCore<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddSignInManager()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("Default");

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromSeconds(300);
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            }).AddCookie(IdentityConstants.ApplicationScheme, opt =>
            {
                opt.ExpireTimeSpan = TimeSpan.FromHours(24);
                opt.LoginPath = "/Usuarios";
                opt.AccessDeniedPath = "/Usuarios/AccessDenied";
            });
            #endregion

            #region Password Security
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = false; // No requiere números
                options.Password.RequiredLength = 1; // Permite cualquier longitud
                options.Password.RequireNonAlphanumeric = false; // No requiere caracteres especiales
                options.Password.RequireUppercase = false; // No requiere mayúsculas
                options.Password.RequireLowercase = false; // No requiere minúsculas
            });
            #endregion
        }

        public static async Task RunIdentitySeeds(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider;

                try
                {
                    var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

                    await DefaultRoles.SeedAsync(userManager, roleManager);
                    await DefaultBasicUser.SeedAsync(userManager, roleManager);
                    await SuperAdminUser.SeedAsync(userManager, roleManager);

                }
                catch (Exception ex)
                {
                }
            }
        }

        public static void AddIdentityService(this IServiceCollection services) 
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IProfileService,  ProfileService>();
            services.AddTransient<EmailHelper>();
        }

    }
}


