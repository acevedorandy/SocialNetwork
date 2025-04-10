

using Microsoft.AspNetCore.Http;

namespace SocialNetwork.Application.Dtos.identity.account
{
    public class PerfilDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string ConfirmarContraseña { get; set; }
        public string Foto { get; set; }
        public IFormFile File { get; set; }
    }
}
