using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.areas.Admin.Controllers
{
    public class ContactMessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
