using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseLanguageController : Controller
    {
        private readonly EduHomeDbContext _context;

        public CourseLanguageController(EduHomeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<CourseLanguage> courseLanguages = await _context.CourseLanguages
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            return View(courseLanguages);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseLanguage courseLanguage)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.CourseLanguages.Add(courseLanguage);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            CourseLanguage? courseLanguage = await _context.CourseLanguages
                .Where(c => !c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();

            if (courseLanguage == null)
            {
                return NotFound();
            }

            return View(courseLanguage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, CourseLanguage courseLanguage)
        {
            CourseLanguage? updatedCourseLanguage = await _context.CourseLanguages
                .Where(c => !c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();

            if (updatedCourseLanguage == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(updatedCourseLanguage);
            }

            updatedCourseLanguage.Name = courseLanguage.Name;
            updatedCourseLanguage.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            CourseLanguage? courseLanguage = await _context.CourseLanguages
                .Where(c => !c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();

            if (courseLanguage == null)
            {
                return NotFound();
            }

            courseLanguage.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
