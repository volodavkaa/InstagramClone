using InstagramClone.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InstagramClone.Controllers
{
    public class DatabaseController : Controller
    {

        public IActionResult UpdateDatabase()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=InstagramCloneDb;Trusted_Connection=True;";
            string sqlQuery = "ALTER TABLE Users ALTER COLUMN ConfirmPassword NVARCHAR(100) NULL;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        ViewBag.Message = "Ñòîâï÷èê ConfirmPassword óñï³øíî çì³íåíî.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Ñòàëàñÿ ïîìèëêà: " + ex.Message;
                }
            }

            return View();
        }
    }
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())  // Важливо мати правильний Claim
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");  // Перенаправляємо на сторінку профілю
                }

                ModelState.AddModelError(string.Empty, "Невірний логін або пароль.");
            }

            return View(model);
        }

        // GET: Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }


        // POST: Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // GET: Account/AccessDenied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        

    }
}