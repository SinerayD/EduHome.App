using EduHome.App.Extensions;
using EduHome.App.Helpers;
using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class TeacherController : Controller
    {
        private readonly EduHomeDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeacherController(EduHomeDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Teacher> teachers = await _context.Teachers
                .Include(t => t.Position)
                .Include(t => t.TeacherHobbies).ThenInclude(th => th.Hobby)
                .Include(t => t.Degree)
                .Include(t => t.Skills)
                .Include(t => t.Socials)

                .ToListAsync();

            return View(teachers);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = await _context.Positions.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Hobbies = await _context.Hobbies.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Degrees = await _context.Degrees.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Skills = await _context.Skills.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Socials = await _context.Socials.Where(x => !x.IsDeleted).ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            ViewBag.Positions = await _context.Positions.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Hobbies = await _context.Hobbies.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Degrees = await _context.Degrees.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Skills = await _context.Skills.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Socials = await _context.Socials.Where(x => !x.IsDeleted).ToListAsync();

            if (teacher.FormFile == null)
            {
                ModelState.AddModelError(nameof(Teacher.FormFile), "The image is required.");
                return View();
            }

            if (!Helper.IsImage(teacher.FormFile))
            {
                ModelState.AddModelError(nameof(Teacher.FormFile), "The selected file is not a valid image.");
                return View();
            }

            if (!Helper.IsSizeOk(teacher.FormFile, 1))
            {
                ModelState.AddModelError(nameof(Teacher.FormFile), "The image size cannot exceed 1MB.");
                return View();
            }


            teacher.Image = teacher.FormFile.CreateImage(_env.WebRootPath, "/assets/img/teacher");

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Positions = await _context.Positions.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Hobbies = await _context.Hobbies.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Degrees = await _context.Degrees.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Skills = await _context.Skills.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Socials = await _context.Socials.Where(x => !x.IsDeleted).ToListAsync();

            Teacher? teacher = await _context.Teachers
                .Include(t => t.Position)
                .Include(t => t.TeacherHobbies)
                .ThenInclude(th => th.Hobby)
                .Include(t => t.Degree)
                .Include(t => t.Skills)
                .Include(t => t.Socials)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
                return NotFound();

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Teacher teacher)
        {
            ViewBag.Positions = await _context.Positions.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Hobbies = await _context.Hobbies.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Degrees = await _context.Degrees.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Skills = await _context.Skills.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Socials = await _context.Socials.Where(x => !x.IsDeleted).ToListAsync();

            Teacher? existingTeacher = await _context.Teachers
                .Include(t => t.Position)
                .Include(t => t.TeacherHobbies)
                .ThenInclude(th => th.Hobby)
                .Include(t => t.Degree)
                .Include(t => t.Skills)
                .Include(t => t.Socials)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (existingTeacher == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(existingTeacher);

            if (teacher.FormFile != null)
            {
                bool isImage = Helper.IsImage(teacher.FormFile);
                bool isSizeOk = Helper.IsSizeOk(teacher.FormFile, 1);

                if (!isImage)
                {
                    ModelState.AddModelError("FormFile", "The selected file is not a valid image.");
                    return View(existingTeacher);
                }

                if (!isSizeOk)
                {
                    ModelState.AddModelError("FormFile", "The image size cannot exceed 1MB.");
                    return View(existingTeacher);
                }

                existingTeacher.Image = teacher.FormFile.CreateImage(_env.WebRootPath, "/assets/img/teacher");
            }


            existingTeacher.PositionId = teacher.PositionId;
            existingTeacher.FullName = teacher.FullName;
            existingTeacher.UpdatedDate = DateTime.Now;
            existingTeacher.TeacherHobbies = teacher.TeacherHobbies;
            existingTeacher.Skills = teacher.Skills;
            existingTeacher.Socials = teacher.Socials;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Teacher? teacher = await _context.Teachers.FindAsync(id);
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
