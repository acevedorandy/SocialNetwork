using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Infrastructure.Interfaces;
using SocialNetwork.Infrastructure.Services;
using SocialNetwork.Infrastructure.Settings;


namespace SocialNetwork.IOC.Dependencies.infraestructure
{
    public static class EmailDependency
    {
        public static void AddEmailDependency(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }

    }
}
