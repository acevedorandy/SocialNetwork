

namespace SocialNetwork.Application.Dtos.dbo
{
    public class AmigosDto
    {
        public int AmigoID { get; set; }
        public string UsuarioID1 { get; set; }
        public string UsuarioID2 { get; set; }
        public DateTime? FechaAmistad { get; set; } = DateTime.Now;
    }
}
