using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using DTO;
using Microsoft.AspNetCore.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller that offers all base functions
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ICustomerManager CustomerManager { get; }
        public IRestaurantManager RestaurantManager { get; }
        public ICourierManager CourierManager { get; }
        public IUtilities Utilities { get; }
        public IDeliveryAreaManager DeliveryAreaManager { get; set; }

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="customerManager">The customer manager object</param>
        /// <param name="restaurantManager">The restaurant manager object</param>
        /// <param name="courierManager">The courier manager object</param>
        /// <param name="utilities">The utilities object</param>
        /// <param name="deliveryAreaManager">The delivery area manager object</param>
        public HomeController(ILogger<HomeController> logger, ICustomerManager customerManager, IRestaurantManager restaurantManager,
                                ICourierManager courierManager, IUtilities utilities, IDeliveryAreaManager deliveryAreaManager)
        {
            _logger = logger;
            CustomerManager = customerManager;
            RestaurantManager = restaurantManager;
            CourierManager = courierManager;
            Utilities = utilities;
            DeliveryAreaManager = deliveryAreaManager;
        }

        /// <summary>
        /// Function to call the default view
        /// </summary>
        /// <returns>Create a an HTML page corresponding to a list of restaurants</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Function to call the privacy page
        /// </summary>
        /// <returns>Create a an HTML page corresponding to a privacy page</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Function that is called in case of error
        /// </summary>
        /// <returns>Create a an HTML page corresponding to a error message</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Function that log out the user
        /// </summary>
        /// <returns>Redirects to Index of Customer controller</returns>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Customer");
        }

        /// <summary>
        /// Function that allows a user to sign up
        /// </summary>
        /// <returns>Create a an HTML page corresponding to a sign up page</returns>
        public IActionResult CustomerSignUp()
        {
            return View();
        }

        /// <summary>
        /// HTTP post to get a customer sign up details
        /// </summary>
        /// <param name="customerViewModel">Customer details entered by the user</param>
        /// <returns>Create a an HTML page corresponding to a login page. Redirects to Sign up in case of error</returns>
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
        /// <summary>
        /// Function that allows a user to sign up
        /// </summary>
        /// <returns>Create a an HTML page corresponding to a sign up page</returns>
        public IActionResult CourierSignUp()
        {
            return View();
        }
        /// <summary>
        /// HTTP post to get a courier sign up details
        /// </summary>
        /// <param name="courierViewModel">Customer details entered by the user</param>
        /// <returns>Create a an HTML page corresponding to a login page. Redirects to Sign up in case of error</returns>
        [HttpPost]
        public IActionResult CourierSignUp(CourierSignUpViewModel courierViewModel)
        {
            if (!ModelState.IsValid) return View("CourierSignUp");
            if (Utilities.IsEmailAddressInDatabase(courierViewModel.EmailAddress))
            {
                ModelState.AddModelError("", "An account with this email address already exists");
                return View("CourierSignUp");
            }
            if (!courierViewModel.Password.Equals(courierViewModel.PasswordConfirmation))
            {
                ModelState.AddModelError("", "Passwords do not match");
                return View("CourierSignUp");
            }
            if (!Utilities.IsPasswordSyntaxCorrect(courierViewModel.Password))
            {
                ModelState.AddModelError("", "Password must have at least 1 digit, 1 capital character, and be 8 characters long");
                return View("CourierSignUp");
            }

            DeliveryArea deliveryArea = DeliveryAreaManager.GetDeliveryAreaByPostcode(courierViewModel.PostCode);
            if (deliveryArea == null)
            {
                ModelState.AddModelError("", "No delivery area with this postcode could be found!");
                return View("CourierSignUp");
            }
            CourierManager.AddCourier(deliveryArea.IdArea, courierViewModel.FirstName, courierViewModel.LastName, courierViewModel.EmailAddress, courierViewModel.Password);

            return View("Login");
        }

        /// <summary>
        /// Function to log in
        /// </summary>
        /// <returns>Create a an HTML page corresponding to a login page</returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// HTTP post to get login information
        /// </summary>
        /// <param name="userViewModel">The login details entered by the user</param>
        /// <returns>Create a an HTML page corresponding to the index corresponding to the type of user signed in</returns>
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
