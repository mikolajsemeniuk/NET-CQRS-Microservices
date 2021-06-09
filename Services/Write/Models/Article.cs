using System;
using System.ComponentModel.DataAnnotations;

namespace Write.Models
{
    public class Article
    {
        [Key] 
        public int ArticleId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}