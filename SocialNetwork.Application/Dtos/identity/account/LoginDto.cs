

using SocialNetwork.Application.Dtos.identity.account.Base;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Application.Dtos.identity.account
{
    public class LoginDto : BaseAccountDto
    {
        [Required(ErrorMessage = "Debe colocar el correo del usuario.")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
