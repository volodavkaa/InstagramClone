using InstagramClone.Models;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    public async Task<IActionResult> EditProfile(IFormFile profilePicture, string profileHeading, string profileBio)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
        if (userIdClaim == null)
        {
            return RedirectToAction("Login", "Account");
        }

        if (!int.TryParse(userIdClaim.Value, out var userId))
        {
            return BadRequest("Invalid User ID");
        }

        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        if (profilePicture != null)
        {
            using (var ms = new MemoryStream())
            {
                await profilePicture.CopyToAsync(ms);
                user.ProfilePicture = ms.ToArray();
            }
        }

        user.ProfileHeading = profileHeading ?? user.ProfileHeading ?? string.Empty; // Запобігаємо NULL для ProfileHeading
        user.ProfileBio = profileBio ?? user.ProfileBio ?? string.Empty; // Запобігаємо NULL для ProfileBio

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
