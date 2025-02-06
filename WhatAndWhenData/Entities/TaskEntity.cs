using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatAndWhenData.Entities;

namespace WhatAndWhenData.Entities
{
    [Table("task")]
    public class TaskEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        [Required]
        public string Description { get; set; }

        [Column("deadline")]
        [Required]
        public DateTime Deadline { get; set; }

        [MaxLength(20)]
        [Required]
        public string Status { get; set; }

        // Relacja z CategoryEntity
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryEntity Category { get; set; }

        // Relacja z PriorityEntity
        public int PriorityId { get; set; }
        [ForeignKey("PriorityId")]
        public PriorityEntity Priority { get; set; }
       
        
    }
}
