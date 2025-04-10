

namespace SocialNetwork.Persistance.Models.ViewModel.dbo
{
    public class MyPostViewModel
    {
        public int PublicacionID { get; set; }
        public string Usuario { get; set; }
        public string FotoUsuario { get; set; }
        public string Contenido { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string Imagen { get; set; }
        public string Video { get; set; }

    }
}
