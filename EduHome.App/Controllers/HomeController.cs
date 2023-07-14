﻿using Microsoft.AspNetCore.Mvc;

namespace Fiorello.App.areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

