using EduHome.App.ViewModel;
using EduHome.Core.Entities;
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
        public async Task<IActionResult> Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Courses = await _context.Courses.Where(x => !x.IsDeleted)
                    .Include(x => x.CourseCategories)
                        .ThenInclude(cc => cc.Category) 
                    .Include(x => x.CourseAssets)
                    .Include(x => x.CourseTags)
                    .Include(x => x.CourseLanguage)
                    .ToListAsync(),

                Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync(),

                Teachers = await _context.Teachers.Where(x => !x.IsDeleted)
                    .Include(x => x.Position)
                    .Include(x => x.Socials)
                    .ToListAsync(),

                Blogs = await _context.Blogs.Where(x => !x.IsDeleted).ToListAsync(),
            };
            return View(homeViewModel);
        }

    }
}

