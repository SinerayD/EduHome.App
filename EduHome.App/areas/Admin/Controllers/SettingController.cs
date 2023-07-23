using EduHome.App.Extensions;
using EduHome.App.Helpers;
using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly EduHomeDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public SettingController(EduHomeDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            List<Setting> settings = await _context.Settings.Where(s=>!s.IsDeleted).ToListAsync();
            return View(settings);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (setting.FormFile is null || setting.FormFile.Length == 0)
            {
                ModelState.AddModelError("", "Image must be added!");
                return View(setting);
            }

            if (!Helper.IsImage(setting.FormFile))
            {
                ModelState.AddModelError("", "The format of the file is not an image!");
                return View(setting);
            }

            if (!Helper.IsSizeOk(setting.FormFile, 1))
            {
                ModelState.AddModelError("", "The size of the image must be less than 1MB!");
                return View(setting);
            }

            setting.AboutImage= setting.FormFile.CreateImage(_environment.WebRootPath, "assets/img/about");
            setting.CreatedDate = DateTime.Now;
            await _context.Settings.AddAsync(setting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Setting? existingSetting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);

            if (existingSetting == null)
            {
                return NotFound();
            }

            return View(existingSetting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Setting setting)
        {
            Setting? existingSetting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

            if (existingSetting == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(existingSetting);
            }
            if (setting.FormFile is null)
            {
                ModelState.AddModelError("", "Image must be added!");
                return View(setting);
            }

            if (!Helper.IsImage(setting.FormFile))
            {
                ModelState.AddModelError("", "The format of the file is not an image!");
                return View(setting);
            }

            if (!Helper.IsSizeOk(setting.FormFile, 1))
            {
                ModelState.AddModelError("", "The size of the image must be less than 1MB!");
                return View(setting);
            }

            existingSetting.AboutTitle = setting.AboutTitle;
            existingSetting.AboutText = setting.AboutText;
            existingSetting.VideoLink = setting.VideoLink;
            existingSetting.Address = setting.Address;
            existingSetting.Email = setting.Email;
            existingSetting.PhoneNumber = setting.PhoneNumber;
            existingSetting.HeaderLogo = setting.HeaderLogo;
            existingSetting.FooterLogo = setting.FooterLogo;
            existingSetting.AboutImage = setting.AboutImage;

            _context.Settings.Update(existingSetting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Setting? setting = await _context.Settings.Where(s => !s.IsDeleted && s.Id == id).FirstOrDefaultAsync();

            if (setting == null)
            {
                return NotFound();
            }

            setting.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

