﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingProject.Data;
using ShoppingProject.Models;
using System.Diagnostics;

namespace ShoppingProject.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShoppingDbContext dbContext;

        public HomeController(ILogger<HomeController> logger, ShoppingDbContext dbContext)
        {
            _logger = logger;
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

            var stores = storeQuery.OrderByDescending(s => s.Id)
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