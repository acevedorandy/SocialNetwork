

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Domain.Entities.dbo
{
    [Table("Amigos", Schema = "dbo")]
    public class Amigos
    {
        [Key]
        public int AmigoID { get; set; }
        public string UsuarioID1 { get; set; }
        public string UsuarioID2 { get; set; }
        public DateTime? FechaAmistad { get; set; }
    }
}
