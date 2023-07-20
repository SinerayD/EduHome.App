using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fir.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly EduHomeDbContext _context;

        public SkillController(EduHomeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Skill> skills = await _context.Skills
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            return View(skills);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Teachers = await _context.Teachers.Where(x => !x.IsDeleted).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Teachers = await _context.Teachers.Where(x => !x.IsDeleted).ToListAsync();
                return View();
            }

            skill.CreatedDate = DateTime.Now;
            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Skill? skill = await _context.Skills
                .Where(x => !x.IsDeleted && x.Id == id)
                .FirstOrDefaultAsync();

            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Skill skill)
        {
            Skill updatedSkill = await _context.Skills
                .Where(x => !x.IsDeleted && x.Id == id)
                .FirstOrDefaultAsync();

            if (updatedSkill == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(updatedSkill);
            }

            updatedSkill.UpdatedDate = DateTime.Now;
            updatedSkill.Name = skill.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Skill skill = await _context.Skills
                .Where(x => !x.IsDeleted && x.Id == id)
                .FirstOrDefaultAsync();

            if (skill == null)
            {
                return NotFound();
            }

            skill.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

