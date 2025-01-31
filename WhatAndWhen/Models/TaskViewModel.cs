using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WhatAndWhen.Models
{
    public class TaskViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Task name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Deadline is required!")]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Status is required!")]
        public string Status { get; set; }
    }
}
