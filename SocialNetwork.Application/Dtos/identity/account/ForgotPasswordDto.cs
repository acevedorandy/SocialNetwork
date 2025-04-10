using SocialNetwork.Application.Dtos.identity.account.Base;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Application.Dtos.identity.account
{
    public class ForgotPasswordDto : BaseAccountDto
    {
        [Required(ErrorMessage = "Debe colocar su correo.")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

    }
}
