﻿using System;
using InstagramClone.Models;

namespace InstagramClone.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime CreatedAt { get; set; }

        // Власник поста
        public int UserId { get; set; }
        public User User { get; set; } = new User();
    }

}
