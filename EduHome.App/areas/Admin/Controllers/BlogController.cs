using EduHome.App.Extensions;
using EduHome.App.Helpers;
using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.areas.Admin.Controllers
{ 
    [Area("Admin")]
     public class BlogController : Controller
{
    private readonly EduHomeDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public BlogController(EduHomeDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Blog> blogs = await _context.Blogs
            .Include(b => b.BlogCategories)
            .ThenInclude(bc => bc.Category)
            .Include(b => b.BlogTags)
            .ThenInclude(bt => bt.Tag)
            .Where(b => !b.IsDeleted)
            .ToListAsync();

        return View(blogs);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
     {
        ViewBag.Categories =await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
        ViewBag.Tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync();

        return View();
     }

     [HttpPost]
     [ValidateAntiForgeryToken]
     public async Task<IActionResult> Create(Blog blog)
     {
        ViewBag.Categories = _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
        ViewBag.Tags = _context.Tags.Where(t => !t.IsDeleted).ToListAsync();

        if (!ModelState.IsValid)
        {
            return View();
        }

        if (blog.FormFile is null || blog.FormFile.Length == 0)
        {
            ModelState.AddModelError("", "Image must be added!");
            return View(blog);
        }

        if (!Helper.IsImage(blog.FormFile))
        {
            ModelState.AddModelError("", "The format of the file is not an image!");
            return View(blog);
        }

        if (!Helper.IsSizeOk(blog.FormFile, 1))
        {
            ModelState.AddModelError("", "The size of the image must be less than 1MB!");
            return View(blog);
        }

        blog.Image = blog.FormFile.CreateImage(_environment.WebRootPath, "assets/images");

        blog.CreatedDate = DateTime.Now;
        blog.BlogCategories = GetSelectedCategories(blog.CategoryIds);
        blog.BlogTags = GetSelectedTags(blog.TagIds);
        await _context.Blogs.AddAsync(blog);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
     }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        Blog? blog = await _context.Blogs
            .Where(b => !b.IsDeleted && b.Id == id)
            .Include(b => b.BlogCategories)
            .ThenInclude(bc => bc.Category)
            .Include(b => b.BlogTags)
            .ThenInclude(bt => bt.Tag)
            .FirstOrDefaultAsync();

        if (blog is null)
        {
            return NotFound();
        }

        ViewBag.Categories = _context.Categories.Where(c => !c.IsDeleted).ToList();
        ViewBag.Tags = _context.Tags.Where(t => !t.IsDeleted).ToList();

        return View(blog);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, Blog blog)
    {
        Blog? updatedBlog = await _context.Blogs
            .Where(b => !b.IsDeleted && b.Id == id)
            .Include(b => b.BlogCategories)
            .ThenInclude(bc => bc.Category)
            .Include(b => b.BlogTags)
            .ThenInclude(bt => bt.Tag)
            .FirstOrDefaultAsync();

        if (updatedBlog is null)
        {
            return NotFound();
        }

        ViewBag.Categories = _context.Categories.Where(c => !c.IsDeleted).ToList();
        ViewBag.Tags = _context.Tags.Where(t => !t.IsDeleted).ToList();

        if (!ModelState.IsValid)
        {
            return View(updatedBlog);
        }

        if (blog.FormFile is not null && blog.FormFile.Length > 0)
        {
            if (!Helper.IsImage(blog.FormFile))
            {
                ModelState.AddModelError("", "The format of the file is not an image!");
                return View(updatedBlog);
            }

            if (!Helper.IsSizeOk(blog.FormFile, 1))
            {
                ModelState.AddModelError("", "The size of the image must be less than 1MB!");
                return View(updatedBlog);
            }

            updatedBlog.Image = blog.FormFile.CreateImage(_environment.WebRootPath, "assets/images");
        }

        updatedBlog.UpdatedDate = DateTime.Now;
        updatedBlog.Title = blog.Title;
        updatedBlog.Description = blog.Description;
        updatedBlog.Author = blog.Author;
        updatedBlog.BlogCategories = GetSelectedCategories(blog.CategoryIds);
        updatedBlog.BlogTags = GetSelectedTags(blog.TagIds);

        _context.Blogs.Update(updatedBlog);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        Blog? blog = await _context.Blogs
            .Where(b => !b.IsDeleted && b.Id == id)
            .FirstOrDefaultAsync();

        if (blog is null)
        {
            return NotFound();
        }

        blog.IsDeleted = true;
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private List<BlogCategory> GetSelectedCategories(List<int> categoryIds)
    {
        return _context.Categories
            .Where(c => categoryIds.Contains(c.Id) && !c.IsDeleted)
            .Select(c => new BlogCategory
            {
                CategoryId = c.Id,
                CreatedDate = DateTime.Now
            })
            .ToList();
    }

    private List<BlogTag> GetSelectedTags(List<int> tagIds)
    {
        return _context.Tags
            .Where(t => tagIds.Contains(t.Id) && !t.IsDeleted)
            .Select(t => new BlogTag
            {
                TagId = t.Id,
                CreatedDate = DateTime.Now
            })
            .ToList();
    }
}

}

