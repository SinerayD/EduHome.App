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
    public class TagController : Controller
    {
        private readonly EduHomeDbContext _context;

        public TagController(EduHomeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Tag> tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            tag.CreatedDate=DateTime.Now;
           await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Tag? tag = await _context.Tags
                .Where(t=> t.Id==id && !t.IsDeleted)
                .FirstOrDefaultAsync();   

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Tag tag)
        {
            Tag? updatedTag = await _context.Tags
                .Where(t => t.Id == id && !t.IsDeleted)
                .FirstOrDefaultAsync();

            if (tag==null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            updatedTag.Name = tag.Name;
            updatedTag.UpdatedDate = DateTime.Now;  
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            Tag? tag = await _context.Tags.Where(t => t.Id == id && !t.IsDeleted).FirstOrDefaultAsync();

            if (tag == null)
            {
                return NotFound();
            }

            tag.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
