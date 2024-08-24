using Microsoft.AspNetCore.Mvc;

namespace InstagramClone.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
