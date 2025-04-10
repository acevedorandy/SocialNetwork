

using Microsoft.AspNetCore.Http;

namespace SocialNetwork.Application.Dtos.dbo
{
    public class PublicacionesDto
    {
        public int PublicacionID { get; set; }
        public string UsuarioID { get; set; }
        public string Contenido { get; set; }
        public DateTime? FechaPublicacion { get; set; } = DateTime.Now;
        public string? Imagen { get; set; }
        public string? Video { get; set; }
        public IFormFile? File { get; set; }

    }
}
