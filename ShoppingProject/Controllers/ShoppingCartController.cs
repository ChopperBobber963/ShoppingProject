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

        [HttpPost]
        public async Task<IActionResult> AddToCart(AllProductsForm product)
        {
            var user = await userManager.GetUserAsync(User);

            var shoppingCart = dbContext.ShoppingCarts
             .FirstOrDefault(w => user.Id == w.UserId);


            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var savedProduct = dbContext.Products.FirstOrDefault(p => p.Id == product.Id);
            
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    UserId = user.Id,
                };
                dbContext.Add(shoppingCart);
            }

            if (savedProduct == null)
            {
                return NotFound();
            }

            shoppingCart.Products.Add(savedProduct);

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var user = await userManager.GetUserAsync(User);

            var cart = dbContext.ShoppingCarts.FirstOrDefault(w => w.User.Id == user.Id);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = user.Id
                };

            }

            var savedProduct = cart.Products.FirstOrDefault(p => p.Id == id);

            if (savedProduct == null)
            {
                return NotFound();
            }


            cart.Products.Remove(savedProduct);

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
