using Microsoft.AspNetCore.Mvc;

namespace Fiorello.App.areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

