using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly EduHomeDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AboutController(EduHomeDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            About? about = _context.Abouts.FirstOrDefault();
            return View(about);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(About about, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(about);
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/abouts");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                about.Image = uniqueFileName;
            }

            await _context.AddAsync(about);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            About? about = await _context.Abouts.FindAsync(id);

            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, About updatedAbout, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedAbout);
            }

            About? about = await _context.Abouts.FindAsync(id);

            if (about == null)
            {
                return NotFound();
            }

            about.Title = updatedAbout.Title;
            about.Description = updatedAbout.Description;

            if (imageFile != null && imageFile.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/abouts");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                if (!string.IsNullOrEmpty(about.Image))
                {
                    string existingFilePath = Path.Combine(uploadsFolder, about.Image);
                    if (System.IO.File.Exists(existingFilePath))
                    {
                        System.IO.File.Delete(existingFilePath);
                    }
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                about.Image = uniqueFileName;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            About? about = await _context.Abouts.FindAsync(id);

            if (about == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(about.Image))
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/abouts");
                string filePath = Path.Combine(uploadsFolder, about.Image);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

