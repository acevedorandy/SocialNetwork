
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts.dbo;
using SocialNetwork.Application.Contracts.identity;
using SocialNetwork.Application.Dtos.dbo;
using SocialNetwork.Application.Dtos.identity.account;
using SocialNetwork.Application.Model.ViewModel;
using SocialNetwork.Persistance.Models.dbo;
using SocialNetwork.Persistance.Models.ViewModel.dbo;
using SocialNetwork.Web.Helpers.Perfil;
using System.Security.Claims;

namespace SocialNetwork.Web.Controllers.account
{
    public class PerfilController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly PerfilHelper _perfilHelper;
        private readonly IPublicacionesServices _publicacionesServices;
        private readonly IComentariosServices _comentariosServices;

        public PerfilController(IProfileService profileService, PerfilHelper perfilHelper, IPublicacionesServices publicacionesServices, IComentariosServices comentariosServices)
        {
            _profileService = profileService;
            _perfilHelper = perfilHelper;
            _publicacionesServices = publicacionesServices;
            _comentariosServices = comentariosServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GoProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _profileService.GetUserByEmail(userId);

            var mypost = await _publicacionesServices.MyPostAsync();

            var commentResult = await _comentariosServices.GetAllAsync();

            if (result == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            var perfilWithPost = new PerfilWithPostModel
            {
                PerfilViewModel = result, 
                MyPosts = mypost.IsSuccess ? (List<MyPostViewModel>)mypost.Model : new List<MyPostViewModel>(), 
                Comentarios = commentResult.IsSuccess ? (List<ComentariosModel>)commentResult.Model : new List<ComentariosModel>() 
            };

            return View(perfilWithPost);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _profileService.GetUserByEmail(userId);

            if (result == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(PerfilDto perfilDto, IFormFile file)
        {
            var result = await _profileService.UpdateProfile(perfilDto);

            if (result.IsSuccess)
            {
                perfilDto = result.Model;
                perfilDto = await _perfilHelper.UpdatePerfilPhoto(perfilDto, file);
                var updateResult = await _profileService.UpdateProfile(perfilDto);

                if (!updateResult.IsSuccess)
                {
                    result.IsSuccess = false;
                    ViewBag.Message = updateResult.Messages;
                    return View(perfilDto);
                }
            }

            if (!result.IsSuccess)
            {
                result.IsSuccess = false;
                ViewBag.Message = result.Messages;
                return View(perfilDto);
            }
            else
            {
                return RedirectToAction("GoProfile", "Perfil");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPerfilComment(ComentariosDto comentariosDto)
        {
            try
            {
                var result = await _comentariosServices.SaveAsync(comentariosDto);

                if (result.IsSuccess)
                {
                    ViewBag.Messege = "Comentario agregado con éxito.";
                }
                else
                {
                    ViewBag.Messege = "Hubo un error al agregar el comentario.";
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var resultPerfil = await _profileService.GetUserByEmail(userId);
                var mypost = await _publicacionesServices.MyPostAsync();
                var commentResult = await _comentariosServices.GetAllAsync();

                var perfilWithPost = new PerfilWithPostModel
                {
                    PerfilViewModel = resultPerfil,
                    MyPosts = mypost.IsSuccess ? (List<MyPostViewModel>)mypost.Model : new List<MyPostViewModel>(),
                    Comentarios = commentResult.IsSuccess ? (List<ComentariosModel>)commentResult.Model : new List<ComentariosModel>()
                };

                return View("GoProfile", perfilWithPost); 
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPerfilComentReply(ComentariosDto comentariosDto)
        {
            try
            {
                var result = await _comentariosServices.SaveAsync(comentariosDto);

                if (result.IsSuccess)
                {
                    ViewBag.Messege = "Comentario agregado con éxito.";
                }
                else
                {
                    ViewBag.Messege = "Hubo un error al agregar el comentario.";
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var resultPerfil = await _profileService.GetUserByEmail(userId);
                var mypost = await _publicacionesServices.MyPostAsync();
                var commentResult = await _comentariosServices.GetAllAsync();

                var perfilWithPost = new PerfilWithPostModel
                {
                    PerfilViewModel = resultPerfil,
                    MyPosts = mypost.IsSuccess ? (List<MyPostViewModel>)mypost.Model : new List<MyPostViewModel>(),
                    Comentarios = commentResult.IsSuccess ? (List<ComentariosModel>)commentResult.Model : new List<ComentariosModel>()
                };

                return View("GoProfile", perfilWithPost); 
            }
            catch
            {
                return View();
            }
        }


    }
}


