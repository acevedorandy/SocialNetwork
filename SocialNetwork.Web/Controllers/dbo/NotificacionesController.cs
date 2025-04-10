using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Web.Middlewares;

namespace SocialNetwork.Web.Controllers.dbo
{
    [ServiceFilter(typeof(LoginAuthorize))]
    [Authorize(Roles = "Basic")]
    public class NotificacionesController : Controller
    {
        // GET: NotificacionesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NotificacionesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NotificacionesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotificacionesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: NotificacionesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NotificacionesController/Edit/5
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

        // GET: NotificacionesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NotificacionesController/Delete/5
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
