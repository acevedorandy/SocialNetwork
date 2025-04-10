

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Domain.Entities.dbo
{
    [Table("Publicaciones", Schema = "dbo")]

    public class Publicaciones
    {
        [Key]
        public int PublicacionID { get; set; }
        public string UsuarioID { get; set; }
        public string Contenido { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string? Imagen { get; set; }
        public string? Video { get; set; }
    }
}
