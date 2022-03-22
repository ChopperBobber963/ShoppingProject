using Microsoft.AspNetCore.Mvc;
using ShoppingProject.Data;
using ShoppingProject.Data.Models;
using ShoppingProject.Models;
using ShoppingProject.Services;

namespace ShoppingProject.Controllers
{
    public class ProductController:Controller
    {
        private readonly IProductService _productService;
        private readonly ShoppingDbContext _dbContext;
        public ProductController(IProductService productService, ShoppingDbContext dbContext)
        {
            _productService = productService;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> All()
        {
            var data = await _productService.GetAll();
            return View(data);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddProductForm product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var productData = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageURL = product.ImageURL,
                ProductType = product.ProductType
            };
            _dbContext.Products.Add(productData);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
