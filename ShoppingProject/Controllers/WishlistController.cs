using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingProject.Data;
using ShoppingProject.Data.Models;
using ShoppingProject.Models;
using ShoppingProject.Services;

namespace ShoppingProject.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ShoppingDbContext _dbContext;
        private readonly UserManager<User> userManager;
       

        public WishlistController(ShoppingDbContext dbContext,
            UserManager<User> userManager
            )
        {
            _dbContext = dbContext;
            this.userManager = userManager;
            
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            if (user.Wishlist == null)
            {
                user.Wishlist = new Wishlist();
            }

            return View(new UserViewModel(user.Id, user.Wishlist, user.ShoppingCart));
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(AllProductsForm product)
        {
            User? user = await userManager.GetUserAsync(User);

            Wishlist? wishlist = _dbContext.Wishlists
             .FirstOrDefault(w => user.Id == w.UserId);


            if (!ModelState.IsValid)
            {
                return View(product);
            }

            Product? savedProduct = _dbContext.Products.FirstOrDefault(p => p.Id == product.Id);
            

            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserId = user.Id,
                };
                _dbContext.Add(wishlist);
            }

            if (savedProduct == null)
            {
                return NotFound();
            }

            wishlist.Products.Add(savedProduct);
            
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWishlist(int id)
        {
            User? user = await userManager.GetUserAsync(User);

            Wishlist? wishlist = _dbContext.Wishlists.FirstOrDefault(w => w.User.Id == user.Id);
            
            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserId = user.Id,
                };

            }

            Product? savedProduct = wishlist.Products.FirstOrDefault(p => p.Id == id);

            if (savedProduct == null)
            {
                return NotFound();
            }
         

            wishlist.Products.Remove(savedProduct);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
