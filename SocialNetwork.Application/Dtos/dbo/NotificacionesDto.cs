

namespace SocialNetwork.Application.Dtos.dbo
{
    public class NotificacionesDto
    {
        public int NotificacionID { get; set; }
        public string UsuarioID { get; set; }
        public string Mensaje { get; set; }
        public bool Leida { get; set; }
        public DateTime? FechaNotificacion { get; set; }

    }
}
