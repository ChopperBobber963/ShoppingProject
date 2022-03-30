using Microsoft.AspNetCore.Mvc;
using ShoppingProject.Data;
using ShoppingProject.Data.Models;
using System.Collections;


namespace ShoppingProject.Controllers.Api
{
    [ApiController]
    [Route("api/products")]
    public class ProductsApiController : ControllerBase
    {
        private readonly ShoppingDbContext data;

        public ProductsApiController(ShoppingDbContext data)
        {
            this.data = data;
        }

        [HttpGet]
        public IEnumerable GetProduct()
        {
            return this.data.Products.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public IEnumerable GetDetails(int id)
        {
            yield return this.data.Products.Find(id);
        }
    }
}
