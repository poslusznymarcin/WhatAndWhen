using WhatAndWhenData.Entities;
using WhatAndWhen.Models;

namespace WhatAndWhen.Mappers
{
    public class TaskMapper
    {
        public static TaskViewModel FromEntity(TaskEntity entity)
        {
            return new TaskViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Deadline = entity.Deadline,
                Status = entity.Status,
                CategoryId = entity.CategoryId,
                PriorityId = entity.PriorityId
            };
        }

        public static TaskEntity ToEntityModel(TaskViewModel model)
        {
            return new TaskEntity()
            {
                Id = model.Id.HasValue ? model.Id.Value : 0,
                Name = model.Name,
                Description = model.Description,
                Deadline = model.Deadline,
                Status = model.Status,
                // Sprawdzamy, czy CategoryId i PriorityId nie są null
                CategoryId = model.CategoryId != null ? (int)model.CategoryId : 0,
                PriorityId = model.PriorityId != null ? (int)model.PriorityId : 0
            };
        }
    }
}