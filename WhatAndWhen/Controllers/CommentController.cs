using Microsoft.AspNetCore.Mvc;

namespace WhatAndWhen.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
