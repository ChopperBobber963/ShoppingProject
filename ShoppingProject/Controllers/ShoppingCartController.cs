using Microsoft.AspNetCore.Mvc;

namespace ShoppingProject.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
