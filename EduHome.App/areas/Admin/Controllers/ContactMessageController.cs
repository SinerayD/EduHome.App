using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly EduHomeDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ContactController(EduHomeDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ContactMessage> contacts = await _context.ContactMessages
                .Where(x => !x.IsDeleted)
                .ToListAsync();
            return View(contacts);
        }

        public async Task<IActionResult> Delete(int id)
        {
            ContactMessage? contactMessage = await _context.ContactMessages
                .Where(x => !x.IsDeleted && x.Id == id)
                .FirstOrDefaultAsync();
            if (contactMessage == null)
            {
                return NotFound();
            }
            contactMessage.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
