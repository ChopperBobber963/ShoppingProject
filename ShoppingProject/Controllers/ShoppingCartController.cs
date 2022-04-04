using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingProject.Data;
using ShoppingProject.Data.Models;
using ShoppingProject.Models;

namespace ShoppingProject.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingDbContext dbContext;
        private readonly UserManager<User> userManager;

        public ShoppingCartController(ShoppingDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task <IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            if (user.ShoppingCart == null)
            {
                user.ShoppingCart = new ShoppingCart();
            }

            return View(new UserViewModel(user.Id, user.Wishlist, user.ShoppingCart));
        }
    }
}
