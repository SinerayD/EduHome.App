using EduHome.App.ViewModel;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.App.areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly EduHomeDbContext _context;
        public HomeController(EduHomeDbContext context)
        {
            _context = context;
        }
        public async Task <IActionResult> Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            return View(homeViewModel); 

        }
    }
}

