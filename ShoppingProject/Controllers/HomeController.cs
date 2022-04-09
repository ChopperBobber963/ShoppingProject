using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingProject.Data;
using ShoppingProject.Models;
using System.Diagnostics;

namespace ShoppingProject.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        
        private readonly ShoppingDbContext dbContext;

        public HomeController(ShoppingDbContext dbContext)
        {
            
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Stores()
        {
            var storeQuery = dbContext.Stores.AsQueryable();

            var stores = storeQuery.OrderBy(s => s.City)
                .Select(s => new StoreDisplay
                {
                    Id = s.Id,
                    City = s.City,
                    Address = s.Address,
                    WorkHours = s.WorkHours,
                    ClosingHours = s.ClosingHours,
                    Details = s.Details
                })
                .ToList();

            return View(new StoresListingModel
            {
                StoresDisplays = stores
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}