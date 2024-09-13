using InstagramClone.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IO;
using InstagramClone.ViewModels;

namespace InstagramClone.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return NotFound("User is not authenticated.");
            }

            var user = await _context.Users
                .Include(u => u.Posts)
                .Include(u => u.Followers)
                .Include(u => u.Following)
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var model = new UserProfileViewModel
            {
                Username = user.Username,
                ProfilePictureUrl = Convert.ToBase64String(user.ProfilePicture),
                ProfileHeading = user.ProfileHeading,
                ProfileBio = user.ProfileBio,
                Posts = user.Posts.Select(p => new PostViewModel
                {
                    ImageUrl = p.ImageUrl,
                    Content = p.Content
                }).ToList(),
                FollowersCount = user.FollowersCount,
                FollowingCount = user.FollowingCount
            };

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> EditProfile(IFormFile profilePicture, string profileHeading, string profileBio)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user == null)
            {
                return NotFound();
            }

            if (profilePicture != null && profilePicture.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await profilePicture.CopyToAsync(ms);
                    user.ProfilePicture = ms.ToArray();
                }
            }

            if (!string.IsNullOrEmpty(profileHeading))
            {
                user.ProfileHeading = profileHeading;
            }

            if (!string.IsNullOrEmpty(profileBio))
            {
                user.ProfileBio = profileBio;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }

            return RedirectToAction("Index", new { id = user.Id });
        }
    }
}
