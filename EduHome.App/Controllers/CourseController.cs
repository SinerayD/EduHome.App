using EduHome.App.ViewModel;
using EduHome.App.ViewModels;
using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.Controllers
{
    public class CourseController : Controller
    {
        private readonly EduHomeDbContext _context;

        public CourseController(EduHomeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            IEnumerable<Teacher> teachers;

            if (id == null || id == 0)
            {
                teachers = await _context.Teachers
                    .Where(x => !x.IsDeleted)
                    .Include(x => x.Position)
                    .ToListAsync();
            }
            else
            {
                teachers = await _context.Teachers
                    .Where(x => !x.IsDeleted)
                    .Include(x => x.Position)
                    .ToListAsync();
            }

            return View(teachers);
        }
        public async Task<IActionResult> Detail(int id)
        {
            ViewBag.Categories = await _context.Categories
                .Where(x => !x.IsDeleted)
                .Include(x => x.CourseCategories)
                .ThenInclude(x => x.Course)
                .ToListAsync();

            ViewBag.Blogs = await _context.Blogs
                .Where(x => !x.IsDeleted)
                .Include(x => x.BlogCategories)
                .ThenInclude(x => x.Category)
                .Include(x => x.BlogTags)
                .ThenInclude(x => x.Tag)
                .Take(3)
                .ToListAsync();

            ViewBag.Tags = await _context.Tags
                .Where(x => !x.IsDeleted)
                .Include(x => x.CourseTags)
                .ThenInclude(x => x.Course)
                .ToListAsync();

            Course? course = await _context.Courses
                .Where(x => !x.IsDeleted && x.Id == id)
                .Include(x => x.CourseCategories)
                .ThenInclude(x => x.Category)
                .Include(x => x.CourseTags)
                .ThenInclude(x => x.Tag)
                .Include(x => x.CourseLanguage)
                .FirstOrDefaultAsync();

            if (course is null)
            {
                return NotFound();
            }

            CourseViewModel courseViewModel = new CourseViewModel
            {
                course = course
            };

            return View(courseViewModel);
        }
    }
}


