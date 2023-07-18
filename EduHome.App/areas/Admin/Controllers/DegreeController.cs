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
    public class DegreeController : Controller
    {
        private readonly EduHomeDbContext _context;

        public DegreeController(EduHomeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Degree> degrees = await _context.Degrees.
                Where(x=>!x.IsDeleted).
                ToListAsync();
            return View(degrees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Degree degree)
        {
            if (ModelState.IsValid)
            {
                _context.Degrees.Add(degree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(degree);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Degree? degree = await _context.Degrees.FindAsync(id);
            if (degree == null)
            {
                return NotFound();
            }

            return View(degree);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Degree degree)
        {
            if (id != degree.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(degree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(degree);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Degree? degree = await _context.Degrees.FindAsync(id);
            if (degree == null)
            {
                return NotFound();
            }

            _context.Degrees.Remove(degree);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

