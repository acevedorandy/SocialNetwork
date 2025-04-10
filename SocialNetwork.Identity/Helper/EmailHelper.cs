using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SocialNetwork.Identity.Shared.Entities;
using System.Text;

namespace SocialNetwork.Identity.Helper
{
    public class EmailHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public EmailHelper(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> VerificationEmailURL(ApplicationUser user, string origin)
        {
            var encodedToken = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(await _userManager.GenerateEmailConfirmationTokenAsync(user))
            );

            var baseUri = new Uri($"{origin}/Usuarios/ConfirmEmail");

            return QueryHelpers.AddQueryString(baseUri.ToString(), new Dictionary<string, string>
            {
            { "userId", user.Id },
            { "token", encodedToken }
            });
        }

        public async Task<string> ForgotPasswordURL(ApplicationUser user, string origin)
        {
            var encodedToken = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(await _userManager.GeneratePasswordResetTokenAsync(user))
            );

            var baseUri = new Uri($"{origin}/Usuarios/ResetPassword");

            return QueryHelpers.AddQueryString(baseUri.ToString(), new Dictionary<string, string>
    {
        { "token", encodedToken }
    });
        }
    }
}
