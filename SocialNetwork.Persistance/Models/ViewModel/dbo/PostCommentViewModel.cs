

using SocialNetwork.Persistance.Models.dbo;

namespace SocialNetwork.Persistance.Models.ViewModel.dbo
{
    public class PostCommentViewModel
    {
        public IEnumerable<MyPostViewModel> MyPost { get; set; }
        public IEnumerable<ComentariosModel> Comentarios { get; set; }

    }
}
