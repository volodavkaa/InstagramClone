﻿using Microsoft.AspNetCore.Mvc;
using InstagramClone.Models;
using InstagramClone.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InstagramClone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Posts = GetPosts()
            };

            return View(viewModel);
        }

        private List<PostViewModel> GetPosts()
        {
            return _context.Posts
                .Include(p => p.User) 
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new PostViewModel
                {
                    ImageUrl = p.ImageUrl,
                    Content = p.Content,
                    Username = p.User.Username,
                    UserProfilePicture = p.User.ProfilePicture != null && p.User.ProfilePicture.Length > 0
                        ? $"data:image/jpeg;base64,{Convert.ToBase64String(p.User.ProfilePicture)}"
                        : "/images/default-avatar.png",
                    Description = p.Content
                })
                .ToList();
        }



    }
}
