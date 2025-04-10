

namespace SocialNetwork.Persistance.Models.dbo
{
    public class AmigosModel
    {
        public int AmigoID { get; set; }
        public string UsuarioID1 { get; set; }
        public string UsuarioID2 { get; set; }
        public DateTime? FechaAmistad { get; set; }

    }
}
