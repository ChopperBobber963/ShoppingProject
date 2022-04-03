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

            return View(new UserViewModel(user.Id, user.Wishlist));
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(AllProductsForm product)
        {
            var user = await userManager.GetUserAsync(User);

            var wishlist = _dbContext.Wishlists
             .FirstOrDefault(w => user.Id == w.UserId);


            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var savedProduct = _dbContext.Products.FirstOrDefault(p => p.Id == product.Id);
            //var newProduct = new Product
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    ImageURL = product.ImageURL,
            //    Price = product.Price,
            //    ProductType = product.ProductType
            //};

            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserId = user.Id,
                };
                _dbContext.Add(wishlist);
            }

            wishlist.Products.Add(savedProduct);
            
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        
    }
}
