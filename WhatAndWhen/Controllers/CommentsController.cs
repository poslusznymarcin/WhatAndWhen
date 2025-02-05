using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WhatAndWhenData.Entities;
using System.Net.Http;
using System.Threading.Tasks;

namespace WhatAndWhen.Controllers
{
    public class CommentsController : Controller
    {
        // GET: Comments
        public async Task<IActionResult> Index()
        {
            IEnumerable<CommentEntity> comments = null;

            using (var client = new HttpClient())
            {
                // Zmień port na taki, jaki masz u siebie w Visual Studio
                client.BaseAddress = new Uri("http://localhost:5016/api/");
                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("comment");

                if (response.IsSuccessStatusCode)
                {
                    comments = await response.Content.ReadFromJsonAsync<List<CommentEntity>>();
                }
                else //web api sent error response 
                {
                    // log response status here..
                    comments = Enumerable.Empty<CommentEntity>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(comments);
        }
    }
}