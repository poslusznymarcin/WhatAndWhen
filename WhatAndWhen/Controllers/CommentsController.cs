using WhatAndWhenData.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WhatAndWhen.Models;

namespace WebApplication1.Controllers
{
    public class CommentsController : Controller
    {
        // GET: commentsController
        public ActionResult Index()
        {
            IEnumerable<CommentEntity> comments = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5016/api/");
                //HTTP GET
                var responseTask = client.GetAsync("comment");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<List<CommentEntity>>();
                    readTask.Wait();
                    comments = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    comments = Enumerable.Empty<CommentEntity>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(comments);
        }
    }
}
