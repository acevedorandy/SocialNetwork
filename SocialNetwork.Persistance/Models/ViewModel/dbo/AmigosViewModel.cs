

using SocialNetwork.Persistance.Models.dbo;

namespace SocialNetwork.Persistance.Models.ViewModel.dbo
{
    public class AmigosViewModel
    {
        public IEnumerable<MisAmigosViewModel> Friends { get; set; }
        public IEnumerable<FriendsViewModel> BuscadorFriends { get; set; }
        public IEnumerable<PublicacionesModel> Publicaciones { get; set; }
        public IEnumerable<ComentariosModel> Comments { get; set; }
    }
}
