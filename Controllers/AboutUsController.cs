using Microsoft.AspNetCore.Mvc;

namespace MovieVerse.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
