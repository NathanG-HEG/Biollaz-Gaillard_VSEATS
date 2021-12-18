using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ICustomerManager CustomerManager { get; }
        public IRestaurantManager RestaurantManager { get; }
        public ICourierManager CourierManager { get; }
        public IUtilities Utilities { get; }

        public HomeController(ILogger<HomeController> logger, ICustomerManager customerManager, IRestaurantManager restaurantManager,
                                ICourierManager courierManager, IUtilities utilities)
        {
            _logger = logger;
            CustomerManager = customerManager;
            RestaurantManager = restaurantManager;
            CourierManager = courierManager;
            Utilities = utilities;
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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Customer");
        }

        public IActionResult CustomerSignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CustomerSignUp(CustomerSignUpViewModel customerViewModel)
        {
            if (!ModelState.IsValid) return View("CustomerSignUp");
            if (Utilities.IsEmailAddressInDatabase(customerViewModel.EmailAddress))
            {
                ModelState.AddModelError("","An account with this email address already exists");
                return View("CustomerSignUp");
            }
            if (!customerViewModel.Password.Equals(customerViewModel.PasswordConfirmation))
            {
                ModelState.AddModelError("","Passwords do not match");
                return View("CustomerSignUp");
            }

            if (!Utilities.IsPasswordSyntaxCorrect(customerViewModel.Password))
            {
                ModelState.AddModelError("","Password must have at least 1 digit, 1 capital character, and be 8 characters long");
                return View("CustomerSignUp");
            }

            CustomerManager.CreateCustomer(customerViewModel.FirstName, customerViewModel.LastName, customerViewModel.EmailAddress, customerViewModel.Password);

            return View("Login");
        }

        public IActionResult CourierSignUp()
        {
            return View();
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

            Customer customer = CustomerManager.GetCustomerByLogin(emailAddress, password);
            if (customer != null)
            {
                HttpContext.Session.SetInt32("IdMember", customer.IdCustomer);
                HttpContext.Session.SetString("TypeOfUser", "Customer");
                HttpContext.Session.SetString("UserFirstName", customer.FirstName);
                return RedirectToAction("Index", "Customer");
            }

            Courier courier = CourierManager.GetCourierByLogin(emailAddress, password);
            if (courier != null)
            {
                HttpContext.Session.SetInt32("IdMember", courier.IdCourier);
                HttpContext.Session.SetString("TypeOfUser", "Courier");
                HttpContext.Session.SetString("UserFirstName", courier.FirstName);
                return RedirectToAction("Index", "Courier");
            }

            Restaurant restaurant = RestaurantManager.GetRestaurantByLogin(emailAddress, password);
            if (RestaurantManager.GetRestaurantByLogin(emailAddress, password) != null)
            {
                HttpContext.Session.SetInt32("IdMember", restaurant.IdRestaurant);
                HttpContext.Session.SetString("TypeOfUser", "Restaurant");
                HttpContext.Session.SetString("RestaurantName", restaurant.Name);
                return RedirectToAction("Index", "Restaurant");
            }
            ModelState.AddModelError("", "Email or password is incorrect. Try again.");
            return View("Login");
        }
    }
}
