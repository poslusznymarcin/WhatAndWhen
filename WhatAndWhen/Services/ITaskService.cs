using WhatAndWhen.Models;
using WhatAndWhenData.Entities;

namespace WhatAndWhen.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskEntity> GetAllTasks();
        TaskEntity GetTaskById(int? id);
        void CreateTask(TaskEntity task);
        void UpdateTask(TaskEntity task);
        void DeleteTask(int id);
        IEnumerable<CategoryEntity> GetAllCategories();
        IEnumerable<PriorityEntity> GetAllPriorities();
    }
}
