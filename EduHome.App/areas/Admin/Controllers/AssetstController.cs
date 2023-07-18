using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseAssetsController : Controller
    {
        private readonly EduHomeDbContext _context;

        public CourseAssetsController(EduHomeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<CourseAssets> courseAssets = await _context.CourseAssets
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            return View(courseAssets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseAssets courseAssets)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            courseAssets.CreatedDate = DateTime.Now;
            await _context.CourseAssets.AddAsync(courseAssets);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            CourseAssets? courseAssets = await _context.CourseAssets
                .Where(c => !c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();

            if (courseAssets == null)
            {
                return NotFound();
            }

            return View(courseAssets);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, CourseAssets courseAssets)
        {
            CourseAssets? updatedCourseAssets = await _context.CourseAssets
                .Where(c => !c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();

            if (updatedCourseAssets == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(updatedCourseAssets);
            }

            updatedCourseAssets.UpdatedDate = DateTime.Now;
            updatedCourseAssets.Name = courseAssets.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            CourseAssets? courseAssets = await _context.CourseAssets
                .Where(c => !c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();

            if (courseAssets == null)
            {
                return NotFound();
            }

            courseAssets.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }

}

