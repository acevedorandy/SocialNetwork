using SocialNetwork.Persistance.Models.dbo;
using SocialNetwork.Persistance.Models.ViewModel.dbo;

namespace SocialNetwork.Application.Model.ViewModel
{
    public class PerfilWithPostModel
    {
        public PerfilViewModel PerfilViewModel { get; set; } // Para el perfil
        public IEnumerable<MyPostViewModel> MyPosts { get; set; } // Para las publicaciones
        public IEnumerable<ComentariosModel> Comentarios { get; set; }
    }
}
