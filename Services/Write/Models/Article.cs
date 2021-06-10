using System;
using System.ComponentModel.DataAnnotations;

namespace Write.Models
{
    public class Article
    {
        [Key] 
        public Guid ArticleId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}