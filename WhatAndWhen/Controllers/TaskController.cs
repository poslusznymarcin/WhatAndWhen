using Microsoft.AspNetCore.Mvc;
using WhatAndWhen.Models;
using WhatAndWhen.Services;
using WhatAndWhenData.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace WhatAndWhen.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tasks = _taskService.GetAllTasks().Select(t => new TaskViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Deadline = t.Deadline,
                Status = t.Status,
                CategoryId = t.CategoryId,
                CategoryName = t.Category?.Name,
                PriorityId = t.PriorityId,
                PriorityName = t.Priority?.Name
            });
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_taskService.GetAllCategories(), "Id", "Name");
            ViewBag.Priorities = new SelectList(_taskService.GetAllPriorities(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                var taskEntity = new TaskEntity
                {
                    Name = task.Name,
                    Description = task.Description,
                    Deadline = task.Deadline,
                    Status = task.Status,
                    // Używamy operatora ?? do podania wartości domyślnej, jeśli CategoryId lub PriorityId są null
                    CategoryId = task.CategoryId ?? 0,
                    PriorityId = task.PriorityId ?? 0
                };
                _taskService.CreateTask(taskEntity);
                return RedirectToAction("Index");
            }
            // Jeśli ModelState nie jest ważny, odświeżamy listy kategorii i priorytetów
            ViewBag.Categories = new SelectList(_taskService.GetAllCategories(), "Id", "Name", task.CategoryId);
            ViewBag.Priorities = new SelectList(_taskService.GetAllPriorities(), "Id", "Name", task.PriorityId);
            return View(task);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskEntity = _taskService.GetTaskById(id);
            if (taskEntity == null)
            {
                return NotFound();
            }

            var taskViewModel = new TaskViewModel
            {
                Id = taskEntity.Id,
                Name = taskEntity.Name,
                Description = taskEntity.Description,
                Deadline = taskEntity.Deadline,
                Status = taskEntity.Status,
                CategoryId = taskEntity.CategoryId,
                CategoryName = taskEntity.Category?.Name,
                PriorityId = taskEntity.PriorityId,
                PriorityName = taskEntity.Priority?.Name
            };

            ViewBag.Categories = new SelectList(_taskService.GetAllCategories(), "Id", "Name", taskViewModel.CategoryId);
            ViewBag.Priorities = new SelectList(_taskService.GetAllPriorities(), "Id", "Name", taskViewModel.PriorityId);

            return View(taskViewModel);
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
                var taskEntity = new TaskEntity
                {
                    Id = task.Id ?? 0,
                    Name = task.Name,
                    Description = task.Description,
                    Deadline = task.Deadline,
                    Status = task.Status,
                    // Używamy operatora ?? do podania wartości domyślnej, jeśli CategoryId lub PriorityId są null
                    CategoryId = task.CategoryId ?? 0,
                    PriorityId = task.PriorityId ?? 0
                };
                _taskService.UpdateTask(taskEntity);
                return RedirectToAction(nameof(Index));
            }
            // Jeśli ModelState nie jest ważny, odświeżamy listy kategorii i priorytetów
            ViewBag.Categories = new SelectList(_taskService.GetAllCategories(), "Id", "Name", task.CategoryId);
            ViewBag.Priorities = new SelectList(_taskService.GetAllPriorities(), "Id", "Name", task.PriorityId);
            return View(task);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskEntity = _taskService.GetTaskById(id);
            if (taskEntity == null)
            {
                return NotFound();
            }

            var taskViewModel = new TaskViewModel
            {
                Id = taskEntity.Id,
                Name = taskEntity.Name,
                Description = taskEntity.Description,
                Deadline = taskEntity.Deadline,
                Status = taskEntity.Status,
                CategoryId = taskEntity.CategoryId,
                CategoryName = taskEntity.Category?.Name,
                PriorityId = taskEntity.PriorityId,
                PriorityName = taskEntity.Priority?.Name
            };
            return View(taskViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _taskService.DeleteTask(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskEntity = _taskService.GetTaskById(id);
            if (taskEntity == null)
            {
                return NotFound();
            }

            var taskViewModel = new TaskViewModel
            {
                Id = taskEntity.Id,
                Name = taskEntity.Name,
                Description = taskEntity.Description,
                Deadline = taskEntity.Deadline,
                Status = taskEntity.Status,
                CategoryId = taskEntity.CategoryId,
                CategoryName = taskEntity.Category?.Name,
                PriorityId = taskEntity.PriorityId,
                PriorityName = taskEntity.Priority?.Name
            };
            return View(taskViewModel);
        }
    }
}