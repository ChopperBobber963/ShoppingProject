using Microsoft.AspNetCore.Mvc;

namespace ShoppingProject.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
