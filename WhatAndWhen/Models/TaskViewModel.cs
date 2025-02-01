using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WhatAndWhen.Models
{
    public class TaskViewModel
    {
        public TaskViewModel()
        {
            Name = string.Empty;
            Description = string.Empty;
            Status = string.Empty;
            CategoryName = string.Empty;
            PriorityName = string.Empty;
        }

        [HiddenInput]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Task name is required!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Deadline is required!")]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Status is required!")]
        public string? Status { get; set; }

        // Jeśli kategoria jest opcjonalna, użyj int?, jeśli wymagana, pozostaw int
        [Required(ErrorMessage = "Category is required!")]
        public int? CategoryId { get; set; }
        [Display(Name = "Category")]
        public string? CategoryName { get; set; }

        // Jeśli priorytet jest opcjonalny, użyj int?, jeśli wymagany, pozostaw int
        [Required(ErrorMessage = "Priority is required!")]
        public int? PriorityId { get; set; }
        [Display(Name = "Priority")]
        public string? PriorityName { get; set; }
    }
}