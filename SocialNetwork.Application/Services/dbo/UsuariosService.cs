using AutoMapper;
using SocialNetwork.Application.Contracts.dbo;
using SocialNetwork.Application.Contracts.identity;
using SocialNetwork.Application.Dtos.identity;
using SocialNetwork.Application.Dtos.identity.account;
using SocialNetwork.Application.Response;
using SocialNetwork.Identity.Response;


namespace SocialNetwork.Application.Services.dbo
{
    public class UsuariosService : IUsuariosServices
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UsuariosService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto, string origin)
        {
            ForgotPasswordRequest forgotPassword = _mapper.Map<ForgotPasswordRequest>(forgotPasswordDto);
            return await _accountService.ForgotPasswordAsync(forgotPassword, origin);

        }

        public async Task<AuthenticationResponse> LoginAsync(LoginDto loginDto)
        {
            AuthenticationRequest authentication = _mapper.Map<AuthenticationRequest>(loginDto);
            AuthenticationResponse response = await _accountService.AuthenticateAsync(authentication);
            return response;
        }
        
        public async Task<RegisterResponse> RegisterAsync(RegisterDto registerDto, string origin)
        {
            RegisterRequest register = _mapper.Map<RegisterRequest>(registerDto);
            RegisterResponse response = await _accountService.RegisterBasicUserAsync(register, origin);
            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            ResetPasswordRequest passwordRequest = _mapper.Map<ResetPasswordRequest>(resetPasswordDto);
            return await _accountService.ResetPasswordAsync(passwordRequest);
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task UploadPhotoAsync(RegisterDto registerDto)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(registerDto);
            await _accountService.UploadPhoto(registerRequest);
        }
    }
}
