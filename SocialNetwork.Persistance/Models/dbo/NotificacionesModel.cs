

namespace SocialNetwork.Persistance.Models.dbo
{
    public class NotificacionesModel
    {
        public int NotificacionID { get; set; }
        public string UsuarioID { get; set; }
        public string Mensaje { get; set; }
        public bool Leida { get; set; }
        public DateTime? FechaNotificacion { get; set; }

    }
}
