

using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entities.dbo;

namespace SocialNetwork.Persistance.Context
{
    public partial class SocialNetworkContext : DbContext
    {
        public SocialNetworkContext(DbContextOptions<SocialNetworkContext> options) : base(options)
        {
            
        }

        #region
        public DbSet<Amigos> Amigos { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<Notificaciones> Notificaciones { get; set; }
        public DbSet<Publicaciones> Publicaciones { get; set; } 
        public DbSet<Usuarios> Usuarios { get; set; }

        #endregion
    }
}
