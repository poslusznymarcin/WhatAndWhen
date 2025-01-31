using Microsoft.AspNetCore.Mvc;
using WhatAndWhen.Models;

namespace WhatAndWhen.Controllers
{
    public class TaskController : Controller
    {
        static List<TaskViewModel> tasks = new List<TaskViewModel>();

        [HttpGet]
        public IActionResult Index()
        {
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                task.Id = tasks.Any() ? tasks.Max(x => x.Id ?? 0) + 1 : 1;
                tasks.Add(task);
                return RedirectToAction("Index");
            }
            return View(task); // Zwróć widok z błędami walidacji
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = tasks.FirstOrDefault(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TaskViewModel task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Znajdź istniejące zadanie w liście
                var existingTask = tasks.FirstOrDefault(m => m.Id == id);
                if (existingTask != null)
                {
                    // Aktualizuj właściwości istniejącego zadania
                    existingTask.Name = task.Name;
                    existingTask.Description = task.Description;
                    existingTask.Deadline = task.Deadline;
                    existingTask.Status = task.Status;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = tasks.FirstOrDefault(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = tasks.FirstOrDefault(m => m.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = tasks.FirstOrDefault(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }
    }
}
