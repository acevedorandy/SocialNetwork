

using SocialNetwork.Application.Dtos.identity;
using SocialNetwork.Application.Dtos.identity.account;
using SocialNetwork.Application.Model.ViewModel;
using SocialNetwork.Application.Response;
using SocialNetwork.Identity.Response;

namespace SocialNetwork.Application.Contracts.identity
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest authenticationRequest);
        Task SignOutAsync();
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task UploadPhoto(RegisterRequest registerDto);
    }
}
