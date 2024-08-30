using InstagramClone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
            
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");

            if (userIdClaim == null)
            {
                
                return RedirectToAction("AccessDenied", "Account");
            }

            
            int userId = int.Parse(userIdClaim.Value);

            
            return RedirectToAction("Index", new { id = userId });
        }

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
    [HttpPost]
    public async Task<IActionResult> EditProfile(IFormFile profilePicture, string profileHeading, string profileBio)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserID").Value);
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        if (profilePicture != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await profilePicture.CopyToAsync(memoryStream);
                user.ProfilePicture = memoryStream.ToArray();
            }
        }

        user.ProfileHeading = profileHeading;
        user.ProfileBio = profileBio;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            // Логування помилки для подальшого аналізу
            Console.WriteLine(ex.InnerException?.Message);
            throw;
        }

        return RedirectToAction("Index", new { id = user.Id });
    }



}
