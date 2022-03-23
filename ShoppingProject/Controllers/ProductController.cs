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
            var productsQuery = _dbContext.Products.AsQueryable();

            

            var products = productsQuery
                .OrderByDescending(pt => pt.Id)
                .Select(p => new AllProductsForm
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageURL = p.ImageURL,
                    ProductType = p.ProductType
                })
                .ToList();

            return View(new ProductListingModel
            {
                Products = products
            });
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
                ImageURL = product.ImageURL,
                Price = product.Price,
                ProductType = product.ProductType
            };
            _productService.Add(productData);
            
            return RedirectToAction(nameof(All));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _productService.GetById(id);
            if(productDetails == null) return NotFound();
            var gosho = new AddProductForm() 
            { 
                Id = productDetails.Id,
                Name = productDetails.Name,
                Description = productDetails.Description,
                ImageURL= productDetails.ImageURL,
                Price = productDetails.Price,
                ProductType = productDetails.ProductType
                
            };
            return View(gosho);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AllProductsForm product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var productData = new Product
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price,
                ProductType = product.ProductType
            };

            await _productService.Update(id, productData);
            return RedirectToAction(nameof(All));
        }
    }
}
