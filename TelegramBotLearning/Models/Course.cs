using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramBotLearning.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public string InstructorId { get; set; } = string.Empty;

        [ForeignKey("InstructorId")]
        public ApplicationUser? Instructor { get; set; }

        [Required]
        [Column(TypeName = "numeric(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
} 