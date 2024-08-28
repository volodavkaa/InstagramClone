using System;
using System.ComponentModel.DataAnnotations;

namespace InstagramClone.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string Caption { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // Власник поста
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
