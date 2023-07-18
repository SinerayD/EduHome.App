using Microsoft.AspNetCore.Mvc;
using EduHome.App.Extensions;
using EduHome.App.Helpers;
using EduHome.Core.Entities;
using Microsoft.EntityFrameworkCore;
using EduHomeApp.Context;

namespace EduHome.App.areas.Admin.Controllers
{
    [Area("Admin")]
        public class CourseController : Controller
        {
            private readonly EduHomeDbContext _context;
            private readonly IWebHostEnvironment _environment;

            public CourseController(EduHomeDbContext context, IWebHostEnvironment environment)
            {
                _context = context;
                _environment = environment;
            }
        
            public async Task<IActionResult> Index()
            {
                IEnumerable<Course> courses = await _context.Courses
                    .Include(c => c.CourseLanguage)
                    .Include(c => c.CourseAssets)
                    .Include(c => c.CourseCategories)
                    .ThenInclude(cc => cc.Category)
                    .Include(c => c.CourseTags)
                    .ThenInclude(ct => ct.Tag)
                    .Where(c => !c.IsDeleted)
                    .ToListAsync();

                return View(courses);
            }

            [HttpGet]
            public IActionResult Create()
            {
                ViewBag.CourseLanguages = _context.CourseLanguages.Where(cl => !cl.IsDeleted).ToList();
                ViewBag.CourseAssets = _context.CourseAssets.Where(ca => !ca.IsDeleted).ToList();
                ViewBag.Categories = _context.Categories.Where(cat => !cat.IsDeleted).ToList();
                ViewBag.Tags = _context.Tags.Where(tag => !tag.IsDeleted).ToList();

                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Course course)
            {
                ViewBag.CourseLanguages = _context.CourseLanguages.Where(cl => !cl.IsDeleted).ToList();
                ViewBag.CourseAssets = _context.CourseAssets.Where(ca => !ca.IsDeleted).ToList();
                ViewBag.Categories = _context.Categories.Where(cat => !cat.IsDeleted).ToList();
                ViewBag.Tags = _context.Tags.Where(tag => !tag.IsDeleted).ToList();

                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (course.FormFile is null || course.FormFile.Length == 0)
                {
                    ModelState.AddModelError("", "Image must be added!");
                    return View(course);
                }

                if (!Helper.IsImage(course.FormFile))
                {
                    ModelState.AddModelError("", "The format of the file is not an image!");
                    return View(course);
                }

                if (!Helper.IsSizeOk(course.FormFile, 1))
                {
                    ModelState.AddModelError("", "The size of the image must be less than 1MB!");
                    return View(course);
                }

                course.Image = course.FormFile.CreateImage(_environment.WebRootPath, "assets/images");

                course.CreatedDate = DateTime.Now;
                course.CourseCategories = GetSelectedCategories(course.CategoryIds);
                course.CourseTags = GetSelectedTags(course.TagIds);

                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            [HttpGet]
            public async Task<IActionResult> Update(int id)
            {
                Course? course = await _context.Courses
                    .Where(c => !c.IsDeleted && c.Id == id)
                    .Include(c => c.CourseLanguage)
                    .Include(c => c.CourseAssets)
                    .Include(c => c.CourseCategories)
                    .ThenInclude(cc => cc.Category)
                    .Include(c => c.CourseTags)
                    .ThenInclude(ct => ct.Tag)
                    .FirstOrDefaultAsync();

                if (course is null)
                {
                    return NotFound();
                }

                ViewBag.CourseLanguages = _context.CourseLanguages.Where(cl => !cl.IsDeleted).ToList();
                ViewBag.CourseAssets = _context.CourseAssets.Where(ca => !ca.IsDeleted).ToList();
                ViewBag.Categories = _context.Categories.Where(cat => !cat.IsDeleted).ToList();
                ViewBag.Tags = _context.Tags.Where(tag => !tag.IsDeleted).ToList();

                return View(course);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Update(int id, Course course)
            {
                Course? updatedCourse = await _context.Courses
                    .Where(c => !c.IsDeleted && c.Id == id)
                    .Include(c => c.CourseLanguage)
                    .Include(c => c.CourseAssets)
                    .Include(c => c.CourseCategories)
                    .ThenInclude(cc => cc.Category)
                    .Include(c => c.CourseTags)
                    .ThenInclude(ct => ct.Tag)
                    .FirstOrDefaultAsync();

                if (updatedCourse is null)
                {
                    return NotFound();
                }

                ViewBag.CourseLanguages = _context.CourseLanguages.Where(cl => !cl.IsDeleted).ToList();
                ViewBag.CourseAssets = _context.CourseAssets.Where(ca => !ca.IsDeleted).ToList();
                ViewBag.Categories = _context.Categories.Where(cat => !cat.IsDeleted).ToList();
                ViewBag.Tags = _context.Tags.Where(tag => !tag.IsDeleted).ToList();

                if (!ModelState.IsValid)
                {
                    return View(updatedCourse);
                }

                if (course.FormFile is not null && course.FormFile.Length > 0)
                {
                    if (!Helper.IsImage(course.FormFile))
                    {
                        ModelState.AddModelError("", "The format of the file is not an image!");
                        return View(updatedCourse);
                    }

                    if (!Helper.IsSizeOk(course.FormFile, 1))
                    {
                        ModelState.AddModelError("", "The size of the image must be less than 1MB!");
                        return View(updatedCourse);
                    }

                    updatedCourse.Image = course.FormFile.CreateImage(_environment.WebRootPath, "assets/images");
                }

                updatedCourse.UpdatedDate = DateTime.Now;
                updatedCourse.Name = course.Name;
                updatedCourse.Description = course.Description;
                updatedCourse.AboutCourse = course.AboutCourse;
                updatedCourse.Apply = course.Apply;
                updatedCourse.Certification = course.Certification;
                updatedCourse.StartDate = course.StartDate;
                updatedCourse.CourseDuration = course.CourseDuration;
                updatedCourse.ClassDuration = course.ClassDuration;
                updatedCourse.SkillLevel = course.SkillLevel;
                updatedCourse.StudentCount = course.StudentCount;
                updatedCourse.CourseFee = course.CourseFee;
                updatedCourse.CourseLanguageId = course.CourseLanguageId;
                updatedCourse.CourseAssetsId = course.CourseAssetsId;
                updatedCourse.CourseCategories = GetSelectedCategories(course.CategoryIds);
                updatedCourse.CourseTags = GetSelectedTags(course.TagIds);

                _context.Courses.Update(updatedCourse);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            [HttpGet]
            public async Task<IActionResult> Delete(int id)
            {
                Course? course = await _context.Courses
                    .Where(c => !c.IsDeleted && c.Id == id)
                    .FirstOrDefaultAsync();

                if (course is null)
                {
                    return NotFound();
                }

                course.IsDeleted = true;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            public List<CourseCategory> GetSelectedCategories(List<int> categoryIds)
            {
                return _context.Categories
                    .Where(c => categoryIds.Contains(c.Id) && !c.IsDeleted)
                    .Select(c => new CourseCategory
                    {
                        CategoryId = c.Id,
                        CreatedDate = DateTime.Now
                    })
                    .ToList();
            }

            public List<CourseTag> GetSelectedTags(List<int> tagIds)
            {
                return _context.Tags
                    .Where(t => tagIds.Contains(t.Id) && !t.IsDeleted)
                    .Select(t => new CourseTag
                    {
                        TagId = t.Id,
                        CreatedDate = DateTime.Now
                    })
                    .ToList();
            }
        }
    }


