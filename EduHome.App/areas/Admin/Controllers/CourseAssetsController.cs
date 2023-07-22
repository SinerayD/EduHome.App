using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseAssetController : Controller
    {
        private readonly EduHomeDbContext _context;

        public CourseAssetController(EduHomeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<CourseAssets> courseAssets = await _context.CourseAssetss
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
        public async Task<IActionResult> Create(CourseAssets courseAsset)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.CourseAssetss.Add(courseAsset);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            CourseAssets? courseAsset = await _context.CourseAssetss
                .Where(c => !c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();

            if (courseAsset == null)
            {
                return NotFound();
            }

            return View(courseAsset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, CourseAssets courseAsset)
        {
            CourseAssets? updatedCourseAsset = await _context.CourseAssetss
                .Where(c => !c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();

            if (updatedCourseAsset == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(updatedCourseAsset);
            }

            updatedCourseAsset.Name = courseAsset.Name;
            updatedCourseAsset.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            CourseAssets? courseAsset = await _context.CourseAssetss
                .Where(c => !c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();

            if (courseAsset == null)
            {
                return NotFound();
            }

            courseAsset.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
