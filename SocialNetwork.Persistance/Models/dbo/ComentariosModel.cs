

namespace SocialNetwork.Persistance.Models.dbo
{
    public class ComentariosModel
    {
        public int ComentarioID { get; set; }
        public int PublicacionID { get; set; }
        public string Usuario { get; set; }
        public string Foto { get; set; }
        public string UsuarioID { get; set; }
        public string Contenido { get; set; }
        public DateTime? FechaComentario { get; set; }
        public int ComentarioPadreID { get; set; }

    }
}
