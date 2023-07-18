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
    public class HobbyController : Controller
    {
        private readonly EduHomeDbContext _context;

        public HobbyController(EduHomeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Hobby> hobbies = await _context.Hobbies.ToListAsync();
            return View(hobbies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hobby hobby)
        {
            if (ModelState.IsValid)
            {
                _context.Hobbies.Add(hobby);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(hobby);
        }

        public async Task<IActionResult> Update(int id)
        {
            Hobby hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null)
            {
                return NotFound();
            }

            return View(hobby);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Hobby hobby)
        {
            if (id != hobby.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(hobby);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(hobby);
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            Hobby hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null)
            {
                return NotFound();
            }

            _context.Hobbies.Remove(hobby);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

