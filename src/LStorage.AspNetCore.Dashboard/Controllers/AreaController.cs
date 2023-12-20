using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LStorage.AspNetCore.Dashboard.Controllers
{
    public class AreaController : Controller
    {
        // GET: AreaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AreaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AreaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreaController/Create
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

        // GET: AreaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AreaController/Edit/5
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

        // GET: AreaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AreaController/Delete/5
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
