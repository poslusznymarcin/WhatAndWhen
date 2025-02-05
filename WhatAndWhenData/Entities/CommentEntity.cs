using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace WhatAndWhenData.Entities
{
    [Table("comment")]
    public class CommentEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        [Required]
        public string Content { get; set; }

        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        [JsonIgnore]
        public TaskEntity Task { get; set; }
    }
}