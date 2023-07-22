using EduHome.App.Extensions;
using EduHome.App.Helpers;
using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly EduHomeDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public SliderController(EduHomeDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders
                .Where(s => !s.IsDeleted).ToListAsync();
            return View(sliders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (slider.FormFile is null || slider.FormFile.Length == 0)
            {
                ModelState.AddModelError("", "Image must be added!");
                return View(slider);
            }

            if (!Helper.IsImage(slider.FormFile))
            {
                ModelState.AddModelError("", "The format of the file is not an image!");
                return View(slider);
            }

            if (!Helper.IsSizeOk(slider.FormFile, 1))
            {
                ModelState.AddModelError("", "The size of the image must be less than 1MB!");
                return View(slider);
            }

            slider.Image = slider.FormFile.CreateImage(_environment.WebRootPath, "assets/img/slider");
            slider.CreatedDate = DateTime.Now;
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Slider? slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Slider slider)
        {
            Slider? existingSlider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

            if (existingSlider == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(existingSlider);
            }

            if (slider.FormFile is null)
            {
                ModelState.AddModelError("", "Image must be added!");
                return View(slider);
            }

            if (!Helper.IsImage(slider.FormFile))
            {
                ModelState.AddModelError("", "The format of the file is not an image!");
                return View(slider);
            }

            if (!Helper.IsSizeOk(slider.FormFile, 1))
            {
                ModelState.AddModelError("", "The size of the image must be less than 1MB!");
                return View(slider);
            }

            existingSlider.Title = slider.Title;
            existingSlider.Text = slider.Text;
            existingSlider.Link = slider.Link;
            existingSlider.Image = slider.FormFile.CreateImage(_environment.WebRootPath, "assets/img/slider");
            _context.Sliders.Update(existingSlider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Slider? slider = await _context.Sliders.Where(s => !s.IsDeleted && s.Id == id).FirstOrDefaultAsync();

            if (slider == null)
            {
                return NotFound();
            }

            slider.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
