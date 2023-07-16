using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeacherController : Controller
    {
        private readonly EduHomeDbContext _context;

        public TeacherController(EduHomeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers = await _context.Teachers.Include(t => t.Position).ToListAsync();
            return View(teachers);
        }

        public IActionResult Create()
        {
            ViewBag.Positions = _context.Positions.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                if (teacher.FormFile != null && teacher.FormFile.Length > 0)
                {
                    string fileName = Path.GetFileName(teacher.FormFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/teachers", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await teacher.FormFile.CopyToAsync(stream);
                    }
                    teacher.Image = fileName;
                }

                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Positions = _context.Positions.ToList();
            return View(teacher);
        }

        public async Task<IActionResult> Update(int id)
        {
            Teacher teacher = await _context.Teachers.Include(t => t.Position).FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            ViewBag.Positions = _context.Positions.ToList();
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Teacher existingTeacher = await _context.Teachers.FindAsync(id);
                if (existingTeacher == null)
                {
                    return NotFound();
                }

                existingTeacher.FullName = teacher.FullName;
                existingTeacher.Title = teacher.Title;
                existingTeacher.PositionId = teacher.PositionId;

                if (teacher.FormFile != null && teacher.FormFile.Length > 0)
                {
                    string fileName = Path.GetFileName(teacher.FormFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/teachers", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await teacher.FormFile.CopyToAsync(stream);
                    }
                    existingTeacher.Image = fileName;
                }

                _context.Update(existingTeacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Positions = _context.Positions.ToList();
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Teacher teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}