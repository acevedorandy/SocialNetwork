using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts.dbo;
using SocialNetwork.Application.Dtos.identity.account;
using SocialNetwork.Application.Response;
using SocialNetwork.Application.Helpers.web;
using SocialNetwork.Identity.Response;
using SocialNetwork.Web.Helpers.Perfil;
using SocialNetwork.Application.Contracts.identity;


namespace SocialNetwork.Web.Controllers.dbo
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosServices _usuariosServices;
        private readonly PerfilHelper _perfilHelper;
        private readonly IProfileService _profileService;
        private readonly IAccountService _accouuntService;
        public UsuariosController(IUsuariosServices usuariosServices, PerfilHelper perfilHelper, IProfileService profileService, IAccountService accountService)
        {
            _usuariosServices = usuariosServices;
            _perfilHelper = perfilHelper;
            _accouuntService = accountService;
        }

        public ActionResult Home()
        {
            return View();
        }

        public  IActionResult Index()
        {
            return View(new LoginDto());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDto loginDto) 
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            AuthenticationResponse authentication = await _usuariosServices.LoginAsync(loginDto);

            if (authentication != null && authentication.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("usuario", authentication);
                return RedirectToRoute(new { controller = "Publicaciones", action = "Index" });
            }
            else
            {
                loginDto.HasError = authentication.HasError;
                loginDto.Error = authentication.Error;
                return View(loginDto);
            }
        }

        public IActionResult Register()
        {
            return View(new RegisterDto());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return View(registerDto);
            }

            var origen = Request.Headers["origin"];
            RegisterResponse response = await _usuariosServices.RegisterAsync(registerDto, origen);

            if (!response.HasError)
            {
                registerDto = await _perfilHelper.LoadPhoto(registerDto, file);
            }

            if (registerDto.Foto != null)
            {
                await _usuariosServices.UploadPhotoAsync(registerDto);
            }

            if (response.HasError)
            {
                registerDto.HasError = response.HasError;
                registerDto.Error = response.Error;

                return View(registerDto);
            }
            return RedirectToRoute(new { controller = "Usuarios", action = "Index" });
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _usuariosServices.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }

        public async Task<IActionResult> LogOut()
        {
            await _usuariosServices.SignOutAsync();
            HttpContext.Session.Remove("usuario");

            return RedirectToRoute(new { controller = "Usuarios", action = "Home" });
        }

        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPasswordDto);
            }

            var origin = Request.Headers["origin"];

            ForgotPasswordResponse response = await _usuariosServices.ForgotPasswordAsync(forgotPasswordDto, origin);

            if (response.HasError)
            {
                forgotPasswordDto.HasError = response.HasError;
                forgotPasswordDto.Error = response.Error;
                return View(forgotPasswordDto);
            }
            return RedirectToRoute(new { controller = "Usuarios", action = "Index" });
        }

        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPasswordDto { Token = token });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordDto);
            }

            ResetPasswordResponse response = await _usuariosServices.ResetPasswordAsync(resetPasswordDto);

            if (response.HasError)
            {
                resetPasswordDto.HasError = response.HasError;
                resetPasswordDto.Error = response.Error;
                return View(resetPasswordDto);
            }
            return RedirectToRoute(new { controller = "Usuarios", action = "Index" });
        }

        public IActionResult AccessDenied() 
        {
            return View();
        }
    }
}
