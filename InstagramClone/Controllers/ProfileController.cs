using InstagramClone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class ProfileController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProfileController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? id)
    {
        if (id == null)
        {
            // Отримуємо ID поточного користувача з Claims
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");

            if (userIdClaim == null)
            {
                // Якщо не вдалось отримати ID, повертаємо помилку доступу
                return RedirectToAction("AccessDenied", "Account");
            }

            // Конвертуємо значення ID з Claim до типу int
            int userId = int.Parse(userIdClaim.Value);

            // Перенаправляємо на сторінку профілю з підставленим ID
            return RedirectToAction("Index", new { id = userId });
        }

        // Завантаження даних користувача за вказаним ID
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
