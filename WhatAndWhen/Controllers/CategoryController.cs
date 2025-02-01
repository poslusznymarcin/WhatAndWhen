using Microsoft.AspNetCore.Mvc;

namespace WhatAndWhen.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
