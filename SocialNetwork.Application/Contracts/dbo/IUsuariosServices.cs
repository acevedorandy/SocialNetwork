

using SocialNetwork.Application.Dtos.identity;
using SocialNetwork.Application.Dtos.identity.account;
using SocialNetwork.Application.Response;
using SocialNetwork.Identity.Response;

namespace SocialNetwork.Application.Contracts.dbo
{
    public interface IUsuariosServices
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginDto loginDto);
        Task<RegisterResponse> RegisterAsync(RegisterDto registerDto, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task UploadPhotoAsync(RegisterDto registerDto);
        Task SignOutAsync();
    }
}
