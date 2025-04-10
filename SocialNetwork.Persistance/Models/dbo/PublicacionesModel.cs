

namespace SocialNetwork.Persistance.Models.dbo
{
    public class PublicacionesModel
    {
        public int PublicacionID { get; set; }
        public string UsuarioID { get; set; }
        public string Usuario { get; set; }
        public string Foto { get; set; }
        public string Contenido { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string? Imagen { get; set; }
        public string? Video { get; set; }
    }
}
