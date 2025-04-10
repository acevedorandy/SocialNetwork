using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Application.Contracts.dbo;
using SocialNetwork.Application.Mapping.dbo;
using SocialNetwork.Application.Mapping.identity;
using SocialNetwork.Application.Services.dbo;
using SocialNetwork.Persistance.Interfaces.dbo;
using SocialNetwork.Persistance.Repositories.dbo;
using SocialNetwork.Persistance.Validations.dbo;

namespace SocialNetwork.IOC.Dependencies.dbo
{
    public static class DboDependency
    {
        public static void AddDboDependency(this IServiceCollection services)
        {
            // Repositorios
            services.AddScoped<IAmigosRepository, AmigosRepository>();
            services.AddScoped<IComentariosRepository, ComentariosRepository>();
            services.AddScoped<INotificacionesRepository, NotificacionesRepository>();
            services.AddScoped<IPublicacionesRepository, PublicacionesRepository>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();

            // Servicios
            services.AddTransient<IAmigosServices, AmigosService>();
            services.AddTransient<IComentariosServices, ComentariosService>();
            services.AddTransient<INotificacionesServices, NotificacionesService>();
            services.AddTransient<IPublicacionesServices, PublicacionesService>();
            services.AddTransient<IUsuariosServices, UsuariosService>();

            // Dependencias del AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(typeof(DboMapping));
            services.AddAutoMapper(typeof(UserIdentityMapping));

            // Validaciones de campos
            services.AddScoped<AmigosValidations>();
            services.AddScoped<ComentariosValidations>();
            services.AddScoped<NotificacionesValidations>();
            services.AddScoped<PublicacionesValidations>();
            services.AddScoped<UsuariosValidations>();
        }
    }
}
