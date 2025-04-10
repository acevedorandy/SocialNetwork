

namespace SocialNetwork.Application.Dtos.dbo
{
    public class ComentariosDto
    {
        public int ComentarioID { get; set; }
        public int PublicacionID { get; set; }
        public string UsuarioID { get; set; }
        public string Contenido { get; set; }
        public DateTime? FechaComentario { get; set; } = DateTime.Now;
        public int ComentarioPadreID { get; set; }
    }
}
