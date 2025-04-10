

using Microsoft.AspNetCore.Http;

namespace SocialNetwork.Application.Model.ViewModel
{
    public class PublicacionesViewModel
    {
        public int PublicacionID { get; set; }
        public string Usuario { get; set; }
        public string FotoUsuario { get; set; }
        public string Contenido { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string Imagen { get; set; }
        public string? Video { get; set; }
        public IFormFile? File { get; set; }
    }
}
