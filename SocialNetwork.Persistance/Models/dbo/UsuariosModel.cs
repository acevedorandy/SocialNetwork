

namespace SocialNetwork.Persistance.Models.dbo
{
    public class UsuariosModel
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string FotoPerfil { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaRegistro { get; set; } 

    }
}
