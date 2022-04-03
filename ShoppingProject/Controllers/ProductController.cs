using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingProject.Data;
using ShoppingProject.Data.Models;
using ShoppingProject.Models;
using ShoppingProject.Services;

namespace ShoppingProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ShoppingDbContext _dbContext;
        private readonly UserManager<User> userManager;

        public ProductController(IProductService productService, ShoppingDbContext dbContext, UserManager<User> userManager)
        {
            _productService = productService;
            _dbContext = dbContext;
            this.userManager = userManager;
        }
        [AllowAnonymous]
        public IActionResult All(ProductListingModel query)
        {

            ////Get the currently Logged-in User and all its properties - Id, Products, ProductsId, Wishlist, Wishlist Id
            //var user = await userManager.GetUserAsync(User);

            ////Get the wishlist of the logged-in user
            //var wishlist = _dbContext.Wishlists.Where(w => w.Products == user.Wishlist.Products);

            ////Get the products of the logged-in user
            //var userProducts = _dbContext.Products.Where(p => p.UserId == user.Id);

            int currPage = query.CurrentPage;
            int pageSize = 4;
            
            if (currPage <= 0)
            {
                currPage = 1;
            }
            int skipCount = (currPage - 1) * pageSize;

            
            var productsQuery = _dbContext.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                productsQuery = productsQuery.Where(p =>
                p.Name.ToLower().Contains(query.Search.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(query.ProductType))
            {
                productsQuery = productsQuery.Where(p => p.ProductType == query.ProductType);
            }

            var products = productsQuery
                .OrderByDescending(pt => pt.Id)
                .Skip(skipCount)
                .Take(pageSize)
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

            var productCategories = _dbContext.Products
                .Select(p => p.ProductType)
                .Distinct()
                .ToList();

            return View(new ProductListingModel
            {
                Products = products,
                Search = query.Search,
                ProductTypes = productCategories,
                CurrentPage = currPage
            });


        }
        
        //Get Method
        public IActionResult Add()
        {
            return View();
        }

        //Adds products to the Database 
        [Authorize(Roles = DataConstants.Role.Administrator)]
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

        //GETS the product id and gives the user the option to change its attributes 
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _productService.GetById(id);
            
            if (productDetails == null)
            {
                return NotFound(); 
            }

            var productForm = new AddProductForm()
            {
                Id = productDetails.Id,
                Name = productDetails.Name,
                Description = productDetails.Description,
                ImageURL = productDetails.ImageURL,
                Price = productDetails.Price,
                ProductType = productDetails.ProductType

            };

            return View(productForm);
        }

        //Post Method that updates the DB
        [Authorize(Roles = DataConstants.Role.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Edit(AllProductsForm product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var productData = _dbContext.Products.Find(product.Id);

            if (productData == null)
            {
                return NotFound();
            }

            productData.Name = product.Name;
            productData.Description = product.Description;
            productData.ImageURL = product.ImageURL;
            productData.Price = product.Price;
            productData.ProductType = product.ProductType;

            await _productService.Update(productData);
            return RedirectToAction("All", "Product");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var productDetails = await _productService.GetById(id);
            if (productDetails == null) return NotFound();
            var gosho = new AddProductForm()
            {
                Id = productDetails.Id,
                Name = productDetails.Name,
                Description = productDetails.Description,
                ImageURL = productDetails.ImageURL,
                Price = productDetails.Price,
                ProductType = productDetails.ProductType

            };
            return View(gosho);
        }

        [Authorize(Roles = DataConstants.Role.Administrator)]
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteConfirmed([FromForm] int id)
        {
            var productDetails = await _productService.GetById(id);
            if (productDetails == null) return NotFound();

            await _productService.Delete(id);
            return RedirectToAction(nameof(All));
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var product = _productService.Details(id);

            if (product == null)
            {
                return BadRequest();
            }

            return View(product);
        }
    }
}
