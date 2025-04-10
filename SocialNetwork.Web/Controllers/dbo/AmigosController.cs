using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts.dbo;
using SocialNetwork.Application.Dtos.dbo;
using SocialNetwork.Persistance.Models.dbo;
using SocialNetwork.Persistance.Models.ViewModel.dbo;
using SocialNetwork.Web.Middlewares;

namespace SocialNetwork.Web.Controllers.dbo
{
    [ServiceFilter(typeof(LoginAuthorize))]
    [Authorize(Roles = "Basic")]
    public class AmigosController : Controller
    {
        private readonly IAmigosServices _amigosServices;
        private readonly IPublicacionesServices _publicacionesServices;
        private readonly IComentariosServices _comentariosServices;

        public AmigosController(IAmigosServices amigosServices, IPublicacionesServices publicacionesServices, IComentariosServices comentariosServices)
        {
            _amigosServices = amigosServices;
            _publicacionesServices = publicacionesServices;
            _comentariosServices = comentariosServices;
        }

        [HttpGet]
        public async Task<IActionResult> LookFriends()
        {
            AmigosViewModel amigosView = new AmigosViewModel();

           var postResult = await _publicacionesServices.PostByFriendsAsync();
            if (postResult.IsSuccess)
            {
                amigosView.Publicaciones = (List<PublicacionesModel>)postResult.Model;
            }

            var friendResult = await _amigosServices.GetFriendAsync();
            if (friendResult.IsSuccess)
            {
                amigosView.Friends = (List<MisAmigosViewModel>)friendResult.Model;
            }

            var comentsResult = await _comentariosServices.GetAllAsync();
            if (comentsResult.IsSuccess)
            {
                amigosView.Comments = (List<ComentariosModel>)comentsResult.Model;
            }
            return View(amigosView);
        }

        [HttpPost]
        public async Task<IActionResult> LookFriends(string friendsName)
        {
            AmigosViewModel amigosView = new AmigosViewModel();

            if (HttpContext.Request.Method == "POST" && string.IsNullOrEmpty(friendsName))
            {
                ViewBag.Messege = "Este campo no se puede enviar vacío.";

                var postResult = await _publicacionesServices.PostByFriendsAsync();
                if (postResult.IsSuccess)
                {
                    amigosView.Publicaciones = (List<PublicacionesModel>)postResult.Model;
                }

                var friendResult = await _amigosServices.GetFriendAsync();
                if (friendResult.IsSuccess)
                {
                    amigosView.Friends = (List<MisAmigosViewModel>)friendResult.Model;
                }

                var comentsResult = await _comentariosServices.GetAllAsync();
                if (comentsResult.IsSuccess)
                {
                    amigosView.Comments = (List<ComentariosModel>)comentsResult.Model;
                }
                return View(amigosView);
            }

            if (!string.IsNullOrEmpty(friendsName))
            {
                var result = await _amigosServices.LookingForFriendsAsync(friendsName);
                if (result.IsSuccess)
                {
                    amigosView.BuscadorFriends = (List<FriendsViewModel>)result.Model; 
                }
                var postResult = await _publicacionesServices.PostByFriendsAsync();
                if (postResult.IsSuccess)
                {
                    amigosView.Publicaciones = (List<PublicacionesModel>)postResult.Model;
                }
                var friendResult = await _amigosServices.GetFriendAsync();
                if (friendResult.IsSuccess)
                {
                    amigosView.Friends = (List<MisAmigosViewModel>)friendResult.Model;
                }
                var comentsResult = await _comentariosServices.GetAllAsync();
                if (comentsResult.IsSuccess)
                {
                    amigosView.Comments = (List<ComentariosModel>)comentsResult.Model;
                }
            }
            return View(amigosView);
        }

        [HttpPost]
        public async Task<IActionResult> AddFriend(AmigosDto amigosDto)
        {
            var existeRelacion = await _amigosServices.ExistsRelation(amigosDto.UsuarioID2);

            if (existeRelacion)
            {
                TempData["ErrorMessage"] = "No puedes agregar un amigo 2 veces.";
                return RedirectToAction("LookFriends");
            }

            var result = await _amigosServices.SaveAsync(amigosDto);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "Amigo agregado con éxito.";
            }
            else
            {
                TempData["ErrorMessage"] = "Hubo un error al agregar al amigo.";
            }
            return RedirectToAction("LookFriends");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(AmigosDto amigosDto)
        {
            try
            {
                var result = await _amigosServices.RemoveAsync(amigosDto);

                if (result.IsSuccess)
                {
                    ViewBag.Messege = "Usuario eliminado con éxito.";
                }
                else
                {
                    ViewBag.Message = "Hubo un error eliminando el usuario.";
                }
                return RedirectToAction("LookFriends");
            }
            catch
            {
                return View();
            }
        }
    }
}
