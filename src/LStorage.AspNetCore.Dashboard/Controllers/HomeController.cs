using Microsoft.AspNetCore.Mvc;

namespace LStorage.AspNetCore.Dashboard.Controllers
{
    public class HomeController : Controller
    { 
        public IActionResult Index()
        {
            return View();
        } 
    }
}