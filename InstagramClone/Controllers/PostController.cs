﻿using InstagramClone.Models;
using InstagramClone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System;
using System.Security.Claims;

namespace InstagramClone.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imageUrl = string.Empty;

                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    if (file.Length > 0)
                    {
                        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        var filePath = Path.Combine(uploadPath, file.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        imageUrl = $"/uploads/{file.FileName}";
                    }
                }

                // Додаємо логування перед збереженням поста
                Console.WriteLine($"Creating Post with Content: {model.Content}, UserId: {User.FindFirstValue(ClaimTypes.NameIdentifier)}");

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(userId, out var userIdInt))
                {
                    return BadRequest("Invalid User ID");
                }

                var post = new Post
                {
                    Content = model.Content,
                    CreatedAt = DateTime.Now,
                    UserId = userIdInt, // Використовуємо сконвертоване значення
                    ImageUrl = imageUrl
                };

                _context.Posts.Add(post);
                _context.SaveChanges();

                Console.WriteLine("Post successfully saved.");

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

    }
}
