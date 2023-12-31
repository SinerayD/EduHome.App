﻿using EduHome.Core.Entities;
using EduHomeApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HobbyController : Controller
    {
        private readonly EduHomeDbContext _context;

        public HobbyController(EduHomeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Hobby> hobbies = await _context.Hobbies
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            return View(hobbies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hobby hobby)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Hobbies.Add(hobby);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Hobby? hobby = await _context.Hobbies
                .Where(c => !c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();

            if (hobby == null)
            {
                return NotFound();
            }

            return View(hobby);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Hobby hobby)
        {
            Hobby? updatedHobby = await _context.Hobbies
                .Where(c => !c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();

            if (updatedHobby == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(updatedHobby);
            }

            updatedHobby.Name = hobby.Name;
            updatedHobby.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Hobby? hobby = await _context.Hobbies
                .Where(c => !c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();

            if (hobby == null)
            {
                return NotFound();
            }

            hobby.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}


