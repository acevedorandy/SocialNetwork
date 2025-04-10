

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Domain.Entities.dbo
{
    [Table("Comentarios", Schema = "dbo")]

    public class Comentarios
    {
        [Key]
        public int ComentarioID { get; set; }
        public int PublicacionID { get; set; }
        public string UsuarioID { get; set; }
        public string Contenido { get; set; }
        public DateTime? FechaComentario { get; set; }
        public int ComentarioPadreID { get; set; }

    }
}
