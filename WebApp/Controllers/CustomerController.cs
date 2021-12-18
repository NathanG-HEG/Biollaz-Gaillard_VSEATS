using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.CompilerServices;
using WebApp.Models;namespace WebApp.Controllers
{
    public class CustomerController : Controller
    {
        public ICustomerManager CustomerManager { get; }
        public IRestaurantManager RestaurantManager { get; }
        public IOrderManager OrderManager { get; }
        public IDeliveryAreaManager DeliveryAreaManager { get; }
        public IDishManager DishManager { get; }
        public CustomerController(ICustomerManager customerManager, IRestaurantManager restaurantManager,
        IOrderManager orderManager, IDeliveryAreaManager deliveryAreaManager, IDishManager dishManager)
        {
            CustomerManager = customerManager;
            RestaurantManager = restaurantManager;
            OrderManager = orderManager;
            DeliveryAreaManager = deliveryAreaManager;
            DishManager = dishManager;
        }
        public IActionResult Index()
        {
            //if you are not connected you are considered as a client
            if (HttpContext.Session.GetInt32("IdMember") == null)
            {
                HttpContext.Session.SetString("TypeOfUser", "Customer");
            }

            //only client (authenticated or not) can view the list of restaurants
            if (HttpContext.Session.GetString("TypeOfUser") != "Customer")
            {
                return RedirectToAction("Login", "Home");
            }

            //clearing cookies from potential previous orders
            HttpContext.Response.Cookies.Delete("DishesId");
            HttpContext.Response.Cookies.Append("TotalOrder", "0");

            List<Restaurant> restaurants = RestaurantManager.GetAllRestaurants();
            List<RestaurantViewModel> restaurantsVm = new List<RestaurantViewModel>(); 
            foreach (var r in restaurants)
            {
                string deliveryAreaName = DeliveryAreaManager.GetDeliveryAreaById(r.IdArea).Name;
                restaurantsVm.Add(new RestaurantViewModel()
                {
                    IdRestaurant = r.IdRestaurant, AreaName = deliveryAreaName, ImagePath = r.Image, IconPath = r.Logo, Name = r.Name
                });
            }
            return View(restaurantsVm);
        }
        public IActionResult Order(int id)
        {
            if (HttpContext.Session.GetString("TypeOfUser") != "Customer")
            {
                return RedirectToAction("Login", "Home");
            }

            Restaurant r = RestaurantManager.GetRestaurantById(id);
            List<Dish> dishes = DishManager.GetAllDishesByRestaurant(id);
            List<DishViewModel> dishesVm = new List<DishViewModel>();
            foreach (var d in dishes)
            {
                if (d.IsAvailable)
                {
                    dishesVm.Add(new DishViewModel() { IdDish = d.IdDish, Image = d.Image, IdRestaurant = d.IdRestaurant, Name = d.Name, Price = (double)d.Price / 100 });
                }
            }
            string areaName = DeliveryAreaManager.GetDeliveryAreaById(r.IdArea).Name;
            RestaurantViewModel restaurantVm = new RestaurantViewModel()
            {
                IdRestaurant = id, Name = r.Name, AreaName = areaName, Dishes = dishesVm, ImagePath =r.Image, IconPath = r.Logo
            };
            return View(restaurantVm);
        }
        public IActionResult AddToCart(int id)
        {
            if (HttpContext.Session.GetString("TypeOfUser") != "Customer")
            {
                return RedirectToAction("Login", "Home");
            }

            string dishesId = HttpContext.Request.Cookies["DishesId"]; 
            HttpContext.Response.Cookies.Append("DishesId", dishesId + id + "_");

            int totalOrder = Int32.Parse(HttpContext.Request.Cookies["TotalOrder"]);
            int price = DishManager.GetDishById(id).Price;
            HttpContext.Response.Cookies.Append("TotalOrder", (price + totalOrder).ToString());

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult RemoveToCart(int id)
        {
            // Removes 
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Checkout(DishViewModel[] dishes)
        {
            if (HttpContext.Session.GetString("TypeOfUser") != "Customer")
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
    }
}

