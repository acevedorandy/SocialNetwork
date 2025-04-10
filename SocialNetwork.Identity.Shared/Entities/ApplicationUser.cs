using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.Identity.Shared.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Foto { get; set; }
    }
}
