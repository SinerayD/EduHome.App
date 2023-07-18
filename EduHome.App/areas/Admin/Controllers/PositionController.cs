using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly EduHomeDbContext _context;
        public PositionController(EduHomeDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Position> positions = await _context.Positions.
                Where(c => !c.IsDeleted).ToListAsync();
            return View(positions);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Position position)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            position.CreatedDate = DateTime.Now;
            await _context.AddAsync(position);
            await _context.SaveChangesAsync();
            return RedirectToAction("index", "position");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Position? position = await _context.Positions.
                Where(x => !x.IsDeleted && x.Id == id).
                FirstOrDefaultAsync();
            if (position == null)
            {
                return NotFound();
            }
            return View(position);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Position position)
        {
            Position? UpdatePosition = await _context.Positions.
                Where(x => !x.IsDeleted && x.Id == id).
                FirstOrDefaultAsync();
            if (position == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(UpdatePosition);
            }
            UpdatePosition.UpdatedDate = DateTime.Now;
            UpdatePosition.Name = position.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Position? position = await _context.Positions.
                Where(x => !x.IsDeleted && x.Id == id).
                FirstOrDefaultAsync();
            if (position == null)
            {
                return NotFound();
            }
            position.IsDeleted = true;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
