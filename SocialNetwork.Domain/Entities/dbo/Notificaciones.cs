

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Domain.Entities.dbo
{
    [Table("Notificaciones", Schema = "dbo")]

    public class Notificaciones
    {
        [Key]
        public int NotificacionID { get; set; }
        public string UsuarioID { get; set; }
        public string Mensaje { get; set; }
        public bool Leida { get; set; }
        public DateTime? FechaNotificacion { get; set; }
    }
}
