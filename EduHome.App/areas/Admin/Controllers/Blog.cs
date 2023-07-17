using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BlogController : Controller
{
    private readonly EduHomeDbContext _context;

    public BlogController(EduHomeDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Blog> blogs = await _context.Blogs.ToListAsync();
        return View(blogs);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Blog blog)
    {
        if (ModelState.IsValid)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(blog);
    }

    public async Task<IActionResult> Edit(int id)
    {
        Blog blog = await _context.Blogs.FindAsync(id);
        if (blog == null)
        {
            return NotFound();
        }

        return View(blog);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Blog updatedBlog)
    {
        if (id != updatedBlog.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Entry(updatedBlog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(updatedBlog);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        Blog blog = await _context.Blogs.FindAsync(id);
        if (blog == null)
        {
            return NotFound();
        }

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
