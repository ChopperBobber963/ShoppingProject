﻿using Microsoft.AspNetCore.Identity;
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddToWishlist(AllProductsForm product)
        {
            var user = await userManager.GetUserAsync(User);

            Wishlist? wishlist = _dbContext.Wishlists
             .FirstOrDefault(w => user.Id == w.UserId);

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var newProduct = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price,
                ProductType = product.ProductType
            };

            wishlist.Products.Add(newProduct);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));

        }


    }
}
