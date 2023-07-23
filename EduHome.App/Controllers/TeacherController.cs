using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.Controllers
{
    public class TeacherController : Controller
    {
        private readonly EduHomeDbContext _context;

        public TeacherController(EduHomeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            IEnumerable<Teacher> teachers;

            if (id == null || id == 0)
            {
                teachers = await _context.Teachers
                    .Where(x => !x.IsDeleted)
                    .Include(x => x.Position)
                    .ToListAsync();
            }
            else
            {
                teachers = await _context.Teachers
                    .Where(x => !x.IsDeleted)
                    .Include(x => x.Position)
                    .ToListAsync();
            }

            return View(teachers);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Teacher? teacher = await _context.Teachers
         .Where(x => x.Id == id && !x.IsDeleted)
         .Include(x => x.Position)
         .Include(x => x.Degree)
         .Include(x => x.TeacherHobbies)
         .Include(x => x.Skills)
         .Include(x => x.Socials)
         .FirstOrDefaultAsync();

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }
    }
}

