using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatAndWhenData.Entities
{
    [Table("subtask")]
    public class SubtaskEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public TaskEntity Task { get; set; }
    }
}
