﻿using Microsoft.AspNetCore.Mvc;

namespace WebsiteTinTuc.User.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// SPA entry point
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}