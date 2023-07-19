using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscribeController : Controller
    {
        private readonly EduHomeDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public SubscribeController(EduHomeDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment; 
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Subscribe> subscribes = await _context.Subscribes
                .Where(s => !s.IsDeleted)
                .ToListAsync();
            return View(subscribes);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Subscribe? subscribe = await _context.Subscribes.Where(s => s.Id == id && !s.IsDeleted).FirstOrDefaultAsync();

            if (subscribe == null)
            {
                return NotFound();
            }

            subscribe.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
