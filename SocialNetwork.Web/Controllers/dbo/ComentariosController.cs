using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts.dbo;
using SocialNetwork.Application.Dtos.dbo;
using SocialNetwork.Web.Middlewares;

namespace SocialNetwork.Web.Controllers.dbo
{
    [ServiceFilter(typeof(LoginAuthorize))]
    [Authorize(Roles = "Basic")]
    public class ComentariosController : Controller
    {
        private readonly IComentariosServices _comentariosServices;

        public ComentariosController(IComentariosServices comentariosServices)
        {
            _comentariosServices = comentariosServices;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComent(ComentariosDto comentariosDto)
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
                return RedirectToRoute(new { controller = "Publicaciones", action = "Index" });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommentFriendsPost(ComentariosDto comentariosDto)
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
                return RedirectToRoute(new { controller = "Amigos", action = "LookFriends" });
            }
            catch
            {
                return View();
            }
        }

        // GET: ComentariosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ComentariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ComentariosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ComentariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
