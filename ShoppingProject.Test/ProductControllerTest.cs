using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using ShoppingProject.Controllers;
using ShoppingProject.Data.Models;
using ShoppingProject.Services;
using Moq;
using ShoppingProject.Models;

namespace ShoppingProject.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task All_ReturnsAViewResult_WithAListOfProducts()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            var products = new List<Product>();
            products.Add(new Product());
            products.Add(new Product());
            mockProductService.Setup(productService => productService.GetAll()).ReturnsAsync(products);

            var controller = new ProductController(mockProductService.Object);

            // Act
            var result = controller.All(new ProductListingModel());

            // Assert
            Assert.IsNotNull(result);
            var viewResult = (await result) as ViewResult;
            var model = viewResult.ViewData.Model as ProductListingModel;
            Assert.AreEqual(2, model.Products.ToList().Count);
        }

        [Test]
        public async Task Add_Product_Adds_To_The_Db()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            Product addedProduct = null;
            mockProductService.Setup(
                productService => productService.Add(It.IsAny<Product>()))
                .Callback<Product>(p => addedProduct = p);
            var controller = new ProductController(mockProductService.Object);
            var product = new AddProductForm()
            {
                Name = "Starcraft II",
                Description = "Zergs",
                ImageURL = "http://url.com",
                Price = 49.99,
                ProductType = "game",
            };

            // Pre-assert
            Assert.IsNull(addedProduct);

            // Act
            var result = controller.Add(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(addedProduct);
            Assert.AreEqual(product.Name, addedProduct.Name);
            Assert.AreEqual(product.Description, addedProduct.Description);
            Assert.AreEqual(product.ImageURL, addedProduct.ImageURL);
            Assert.AreEqual(product.Price, addedProduct.Price);
            Assert.AreEqual(product.ProductType, addedProduct.ProductType);
        }

        [Test]
        public async Task Edit_Changes_Attributes()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            var starcraft = new Product() {Id = 1, Price = 59.99};

            Product editedProduct = null;
            mockProductService.Setup(productService => productService.GetById(1)).ReturnsAsync(starcraft);
            mockProductService
                .Setup(productService => productService.Update(It.IsAny<Product>()))
                .Callback<Product>(p => starcraft.Price = p.Price);

            var controller = new ProductController(mockProductService.Object);

            var editedStarcraft = new AllProductsForm()
            {
                Id = 1,
                Price = 49.99
            };

            //Act
            var result = controller.Edit(editedStarcraft);

            //Assert
            Assert.AreEqual(49.99, starcraft.Price);
        }
    }
}