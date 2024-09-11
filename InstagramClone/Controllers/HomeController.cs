using Microsoft.AspNetCore.Mvc;
using InstagramClone.Models;

namespace InstagramClone.Controllers
{
    public class HomeController : Controller
    {
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
            
            return new List<PostViewModel>
            {
                new PostViewModel { ImageUrl = "/images/sample1.jpg", Content = "First post!" },
                new PostViewModel { ImageUrl = "/images/sample2.jpg", Content = "Another post!" }
            };
        }
    }
}
