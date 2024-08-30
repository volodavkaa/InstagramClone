using InstagramClone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InstagramClone.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var user = await _context.Users
                .Include(u => u.Followers)
                .Include(u => u.Following)
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var model = new UserProfileViewModel
            {
                Username = user.Username,
                FollowersCount = user.FollowersCount,
                FollowingCount = user.FollowingCount,
                Posts = user.Posts.ToList()
            };

            return View(model);
        }
    }
}
