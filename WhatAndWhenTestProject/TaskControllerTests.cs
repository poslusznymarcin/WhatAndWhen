using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WhatAndWhen.Controllers;
using WhatAndWhen.Models;
using WhatAndWhen.Services;
using WhatAndWhenData.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WhatAndWhenTestProject
{
    [TestFixture]
    public class TaskControllerTests 
    {
        private Mock<ITaskService> _mockTaskService;
        private TaskController _controller;
        


        [SetUp]
        public void Setup()
        {
            _mockTaskService = new Mock<ITaskService>();
            _controller = new TaskController(_mockTaskService.Object);

            // Ustawienie kontrolera do pracy w kontekście testowym
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(_controller.HttpContext, Mock.Of<ITempDataProvider>());
            _controller.TempData = tempData;
        }
        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }
        [Test]
        public void Index_ReturnsViewResult_WithListOfTasks()
        {
            // Arrange
            var mockTasks = new List<TaskEntity>
            {
                new TaskEntity { Id = 1, Name = "Task 1", Description = "Description 1", Deadline = DateTime.Now, Status = "Active", CategoryId = 1, Category = new CategoryEntity { Name = "Category 1" }, PriorityId = 1, Priority = new PriorityEntity { Name = "High" } },
                new TaskEntity { Id = 2, Name = "Task 2", Description = "Description 2", Deadline = DateTime.Now.AddDays(1), Status = "Pending", CategoryId = 2, Category = new CategoryEntity { Name = "Category 2" }, PriorityId = 2, Priority = new PriorityEntity { Name = "Low" } }
            };
            _mockTaskService.Setup(service => service.GetAllTasks()).Returns(mockTasks);

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Index").Or.Null);
            var model = result.Model as IEnumerable<TaskViewModel>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count(), Is.EqualTo(2));
            Assert.That(model.First().Name, Is.EqualTo("Task 1"));
        }

        [Test]
        public void Create_Get_ReturnsViewResult_WithEmptyModel()
        {
            // Arrange
            var mockCategories = new List<CategoryEntity> { new CategoryEntity { Id = 1, Name = "Category 1" } };
            var mockPriorities = new List<PriorityEntity> { new PriorityEntity { Id = 1, Name = "High" } };
            _mockTaskService.Setup(service => service.GetAllCategories()).Returns(mockCategories);
            _mockTaskService.Setup(service => service.GetAllPriorities()).Returns(mockPriorities);

            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Create").Or.Null);
            Assert.That(result.Model, Is.Null);
            Assert.That(_controller.ViewBag.Categories, Is.InstanceOf<SelectList>());
            Assert.That(_controller.ViewBag.Priorities, Is.InstanceOf<SelectList>());
        }

        [Test]
        public void Create_Post_ValidModel_ReturnsRedirectToIndex()
        {
            // Arrange
            var taskViewModel = new TaskViewModel
            {
                Name = "New Task",
                Description = "New Description",
                Deadline = DateTime.Now.AddDays(1),
                Status = "Pending",
                CategoryId = 1,
                PriorityId = 1
            };
            _mockTaskService.Setup(service => service.CreateTask(It.IsAny<TaskEntity>()));

            // Act
            var result = _controller.Create(taskViewModel) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public void Create_Post_InvalidModel_ReturnsViewResult_WithModel()
        {
            // Arrange
            var taskViewModel = new TaskViewModel();
            _controller.ModelState.AddModelError("Name", "Required");
            _mockTaskService.Setup(service => service.GetAllCategories()).Returns(new List<CategoryEntity>());
            _mockTaskService.Setup(service => service.GetAllPriorities()).Returns(new List<PriorityEntity>());

            // Act
            var result = _controller.Create(taskViewModel) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Create").Or.Null);
            Assert.That(result.Model, Is.InstanceOf<TaskViewModel>());
        }

        [Test]
        public void Edit_Get_ValidId_ReturnsViewResult_WithTaskModel()
        {
            // Arrange
            int taskId = 1;
            var taskEntity = new TaskEntity { Id = taskId, Name = "Task 1", Description = "Description 1", Deadline = DateTime.Now, Status = "Active", CategoryId = 1, Category = new CategoryEntity { Name = "Category 1" }, PriorityId = 1, Priority = new PriorityEntity { Name = "High" } };
            _mockTaskService.Setup(service => service.GetTaskById(taskId)).Returns(taskEntity);
            _mockTaskService.Setup(service => service.GetAllCategories()).Returns(new List<CategoryEntity> { taskEntity.Category });
            _mockTaskService.Setup(service => service.GetAllPriorities()).Returns(new List<PriorityEntity> { taskEntity.Priority });

            // Act
            var result = _controller.Edit(taskId) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Edit").Or.Null);
            var model = result.Model as TaskViewModel;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Id, Is.EqualTo(taskId));
        }

        [Test]
        public void Edit_Get_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int taskId = 1;
            _mockTaskService.Setup(service => service.GetTaskById(taskId)).Returns((TaskEntity)null);

            // Act
            var result = _controller.Edit(taskId);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
        [Test]
        public void Edit_Post_ValidModel_ReturnsRedirectToIndex()
        {
            // Arrange
            int taskId = 1;
            var taskViewModel = new TaskViewModel
            {
                Id = taskId,
                Name = "Updated Task",
                Description = "Updated Description",
                Deadline = DateTime.Now.AddDays(1),
                Status = "Completed",
                CategoryId = 1,
                PriorityId = 1
            };
            _mockTaskService.Setup(service => service.UpdateTask(It.IsAny<TaskEntity>()));

            // Act
            var result = _controller.Edit(taskId, taskViewModel) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(_controller.Index)));
        }

        [Test]
        public void Edit_Post_InvalidModel_ReturnsViewResult_WithModel()
        {
            // Arrange
            int taskId = 1;
            var taskViewModel = new TaskViewModel { Id = taskId };
            _controller.ModelState.AddModelError("Name", "Required");
            _mockTaskService.Setup(service => service.GetAllCategories()).Returns(new List<CategoryEntity>());
            _mockTaskService.Setup(service => service.GetAllPriorities()).Returns(new List<PriorityEntity>());

            // Act
            var result = _controller.Edit(taskId, taskViewModel) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Edit").Or.Null);
            Assert.That(result.Model, Is.InstanceOf<TaskViewModel>());
        }

        [Test]
        public void Delete_Get_ValidId_ReturnsViewResult_WithTaskModel()
        {
            // Arrange
            int taskId = 1;
            var taskEntity = new TaskEntity { Id = taskId, Name = "Task 1", Description = "Description 1", Deadline = DateTime.Now, Status = "Active", CategoryId = 1, Category = new CategoryEntity { Name = "Category 1" }, PriorityId = 1, Priority = new PriorityEntity { Name = "High" } };
            _mockTaskService.Setup(service => service.GetTaskById(taskId)).Returns(taskEntity);

            // Act
            var result = _controller.Delete(taskId) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Delete").Or.Null);
            var model = result.Model as TaskViewModel;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Id, Is.EqualTo(taskId));
        }

        [Test]
        public void Delete_Get_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int taskId = 1;
            _mockTaskService.Setup(service => service.GetTaskById(taskId)).Returns((TaskEntity)null);

            // Act
            var result = _controller.Delete(taskId);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void DeleteConfirmed_Post_ValidId_ReturnsRedirectToIndex()
        {
            // Arrange
            int taskId = 1;
            _mockTaskService.Setup(service => service.DeleteTask(taskId));

            // Act
            var result = _controller.DeleteConfirmed(taskId) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo(nameof(_controller.Index)));
        }

        [Test]
        public void Details_Get_ValidId_ReturnsViewResult_WithTaskModel()
        {
            // Arrange
            int taskId = 1;
            var taskEntity = new TaskEntity { Id = taskId, Name = "Task 1", Description = "Description 1", Deadline = DateTime.Now, Status = "Active", CategoryId = 1, Category = new CategoryEntity { Name = "Category 1" }, PriorityId = 1, Priority = new PriorityEntity { Name = "High" } };
            _mockTaskService.Setup(service => service.GetTaskById(taskId)).Returns(taskEntity);

            // Act
            var result = _controller.Details(taskId) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Details").Or.Null);
            var model = result.Model as TaskViewModel;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Id, Is.EqualTo(taskId));
        }

        [Test]
        public void Details_Get_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int taskId = 1;
            _mockTaskService.Setup(service => service.GetTaskById(taskId)).Returns((TaskEntity)null);

            // Act
            var result = _controller.Details(taskId);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
