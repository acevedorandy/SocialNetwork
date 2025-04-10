using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts.dbo;
using SocialNetwork.Application.Dtos.dbo;
using SocialNetwork.Application.Model.ViewModel;
using SocialNetwork.Persistance.Models.dbo;
using SocialNetwork.Persistance.Models.ViewModel.dbo;
using SocialNetwork.Web.Helpers.Perfil;
using SocialNetwork.Web.Middlewares;

namespace SocialNetwork.Web.Controllers.dbo
{
    [ServiceFilter(typeof(LoginAuthorize))]
    [Authorize(Roles = "Basic")]
    public class PublicacionesController : Controller
    {
        private readonly IPublicacionesServices _publicacionesServices;
        private readonly IComentariosServices _comentariosServices;
        private readonly PerfilHelper _perfilHelper;

        public PublicacionesController(IPublicacionesServices publicacionesServices, IComentariosServices comentariosServices,
                                       PerfilHelper perfilHelper)
        {
            _publicacionesServices = publicacionesServices;
            _comentariosServices = comentariosServices;
            _perfilHelper = perfilHelper;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            PostCommentViewModel postCommentView = new PostCommentViewModel();

            var resultPost = await _publicacionesServices.MyPostAsync();
            if (resultPost.IsSuccess)
            {
                postCommentView.MyPost = (List<MyPostViewModel>)resultPost.Model;
            }
            else
            {
                postCommentView.MyPost = new List<MyPostViewModel>();
            }

            var resultComment = await _comentariosServices.GetAllAsync();
            if (resultComment.IsSuccess)
            {
                postCommentView.Comentarios = (List<ComentariosModel>)resultComment.Model;
            }
            else
            {
                postCommentView.Comentarios = new List<ComentariosModel>();
            }

            return View(postCommentView);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PublicacionesDto publicacionesDto, IFormFile file)
        {
            try
            {
                var result = await _publicacionesServices.SaveAsync(publicacionesDto);

                if (result.IsSuccess)
                {
                    publicacionesDto = result.Model;
                    publicacionesDto = await _perfilHelper.UpdatePostPhoto(publicacionesDto, file);
                }
                if (publicacionesDto.Imagen != null && publicacionesDto.PublicacionID > 0)
                {
                    await _publicacionesServices.UpdateAsync(publicacionesDto);
                }

                if (!result.IsSuccess)
                {
                    result.IsSuccess = false;
                    ViewBag.Message = result.Messages;

                    return View(publicacionesDto);
                }
                else
                {
                    return RedirectToAction("Index", "Publicaciones");
                }
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _publicacionesServices.GetByIDAsync(id);

            if (result.IsSuccess)
            {
                PublicacionesViewModel publicacionesModel = (PublicacionesViewModel)result.Model;
                return View(publicacionesModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PublicacionesDto publicacionesDto, IFormFile file)
        {
            var result = await _publicacionesServices.UpdateAsync(publicacionesDto);

            if (result.IsSuccess)
            {
                publicacionesDto = result.Model;
                publicacionesDto = await _perfilHelper.UpdatePostPhoto(publicacionesDto, file);
                var updateResult = await _publicacionesServices.UpdateAsync(publicacionesDto);

                if (!updateResult.IsSuccess)
                {
                    result.IsSuccess = false;
                    ViewBag.Message = result.Messages;
                    return View(publicacionesDto);
                }
            }
            if (!result.IsSuccess)
            {
                result.IsSuccess = false;
                ViewBag.Message = result.Messages;
                return View(publicacionesDto);
            }
            else
            {
                return RedirectToAction("Index", "Publicaciones");
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _publicacionesServices.GetByIDAsync(id);

            if (result.IsSuccess)
            {
                PublicacionesViewModel publicacionesModel = (PublicacionesViewModel)result.Model;
                return View(publicacionesModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PublicacionesDto publicacionesDto)
        {
            try
            {
                var result = await _publicacionesServices.RemoveAsync(publicacionesDto);

                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = result.Messages;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
