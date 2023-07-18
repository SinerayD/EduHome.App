using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NoticeController : Controller
    {
        private readonly EduHomeDbContext _context;

        public NoticeController(EduHomeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Notice> notices = await _context.Notices
                .Where(n => !n.IsDeleted).ToListAsync();
            return View(notices);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notice notice)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            notice.CreatedDate = DateTime.Now;
            await _context.Notices.AddAsync(notice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Notice? notice = await _context.Notices
                .Where(n => n.Id == id && !n.IsDeleted)
                .FirstOrDefaultAsync();

            if (notice == null)
            {
                return NotFound();
            }

            return View(notice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Notice notice)
        {
            Notice? updatedNotice = await _context.Notices
                .Where(n => n.Id == id && !n.IsDeleted)
                .FirstOrDefaultAsync();

            if (updatedNotice == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            updatedNotice.Text = notice.Text;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Notice? notice = await _context.Notices
                .Where(n => n.Id == id && !n.IsDeleted)
                .FirstOrDefaultAsync();

            if (notice is null)
            {
                return NotFound();
            }

            notice.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
