using EduHome.App.ViewModels;
using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.Controllers
{
    public class ContactMessageController : Controller
    {
        private readonly EduHomeDbContext _context;

        public ContactMessageController(EduHomeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ContactMessageViewModel contactMessageViewModel = new ContactMessageViewModel()
            {
                Setting = await _context.Settings.Where(x => !x.IsDeleted).FirstOrDefaultAsync()
            };
            return View(contactMessageViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ContactMessage contactMessage)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            await _context.ContactMessages.AddAsync(contactMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}