using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WhatAndWhen.Models;
using WhatAndWhenData;
using WhatAndWhenData.Entities;

namespace WhatAndWhen.Services
{
    public class TaskService : ITaskService
    {
        private readonly WhatAndWhenContext _context;

        public TaskService(WhatAndWhenContext context)
        {
            _context = context;
        }

        public IEnumerable<TaskEntity> GetAllTasks()
        {
            // Pobieramy wszystkie zadania z uwzględnieniem kategorii i priorytetów
            return _context.Tasks
                .Include(t => t.Category)
                .Include(t => t.Priority)
                .ToList();
        }

        public TaskEntity GetTaskById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            // Pobieramy zadanie z uwzględnieniem kategorii i priorytetów
            return _context.Tasks
                .Include(t => t.Category)
                .Include(t => t.Priority)
                .FirstOrDefault(t => t.Id == id);
        }

        public void CreateTask(TaskEntity task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(TaskEntity task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            // Sprawdzamy, czy zadanie istnieje w bazie danych
            var existingTask = _context.Tasks.AsNoTracking().FirstOrDefault(t => t.Id == task.Id);
            if (existingTask == null)
            {
                throw new KeyNotFoundException("Task not found.");
            }
            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }

        public IEnumerable<CategoryEntity> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public IEnumerable<PriorityEntity> GetAllPriorities()
        {
            return _context.Priorities.ToList();
        }
    }
}