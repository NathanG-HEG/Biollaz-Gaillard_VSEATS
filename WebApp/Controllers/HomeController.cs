using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ICustomerManager CustomerManager { get; }
        public IRestaurantManager RestaurantManager { get; }
        public ICourierManager CourierManager { get; }

        public HomeController(ILogger<HomeController> logger, ICustomerManager customerManager, IRestaurantManager restaurantManager,
                                ICourierManager courierManager)
        {
            _logger = logger;
            CustomerManager = customerManager;
            RestaurantManager = restaurantManager;
            CourierManager = courierManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginViewModel userViewModel)
        {
            if (!ModelState.IsValid) return View("Login");
            string emailAddress = userViewModel.emailAddress;
            string password = userViewModel.password;
            if (CustomerManager.GetCustomerByLogin(emailAddress, password) != null)
            {
                return RedirectToAction("Index", "Customer");
            }

            Courier courier = CourierManager.GetCourierByLogin(emailAddress, password);
            if (courier != null)
            {
                return RedirectToAction("Index", "Courier");
            }

            if (RestaurantManager.GetRestaurantByLogin(emailAddress, password) != null)
            {
                return RedirectToAction("Index", "Restaurant");
            }
            ModelState.AddModelError("", "Email or password is incorrect. Try again.");
            return View("Login");
        }
    }
}
