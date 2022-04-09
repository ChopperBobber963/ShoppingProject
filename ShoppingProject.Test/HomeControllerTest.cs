using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ShoppingProject.Controllers;
using ShoppingProject.Data;
using Moq;
using ShoppingProject.Data.Models;
using ShoppingProject.Models;

namespace ShoppingProject.Test
{
    public class HomeControllerTest
    {
        //private readonly ShoppingDbContext dbContext;

        //public HomeControllerTest(ShoppingDbContext dbContext)
        //{
        //    this.dbContext = dbContext;
        //}
        [Test]
        public async Task IndexView_DisplaysMainPage()
        {
            var options = new DbContextOptionsBuilder<ShoppingDbContext>()
                // .UseInMemoryDatabase(databaseName: "ShoppingProject")
                .Options;

            var context = new ShoppingDbContext(options);

            var controller = new HomeController(context);

            var result = controller.Index();

            Assert.IsInstanceOf(typeof(ViewResult),result);
        }

        [Test]
        public async Task Stores_DisplaysSingleStore_WhenDatabaseContainsSingleStore()
        {
            var options = new DbContextOptionsBuilder<ShoppingDbContext>()
                .UseInMemoryDatabase(databaseName: "ShoppingProject")
                .Options;

            await using (var context = new ShoppingDbContext(options))
            {
                context.Stores.Add(new Store()
                {
                    Id = 1,
                    City = "Sofia",
                    Address = "Vitoshka",
                    WorkHours = "9:00",
                    ClosingHours = "18:00",
                    Details = "In Sofia"
                });
                context.SaveChanges();
            }

            await  using (var context = new ShoppingDbContext(options))
            {
                var controller = new HomeController(context);
                var result = controller.Stores();

                Assert.IsNotNull(result);
                var viewResult = result as ViewResult;
                var model = viewResult.ViewData.Model as StoresListingModel;
                Assert.AreEqual(1, model.StoresDisplays.ToList().Count);

                
            }
        }
       
    }
}
