using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
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
            List<Restaurant> restaurants = RestaurantManager.GetAllRestaurants();
            List<RestaurantViewModel> restaurantsVM = new List<RestaurantViewModel>(); 
            foreach (var r in restaurants)
            {
                string deliveryAreaName = DeliveryAreaManager.GetDeliveryAreaById(r.IdArea).Name;
                restaurantsVM.Add(new RestaurantViewModel()
                {
                    IdRestaurant = r.IdRestaurant, AreaName = deliveryAreaName, ImagePath = r.Image, IconPath = r.Logo, Name = r.Name
                });
            }
            return View(restaurantsVM);
        }
        public IActionResult Order(int id)
        {
            Restaurant r = RestaurantManager.GetRestaurantById(id); List<Dish> dishes = DishManager.GetAllDishesByRestaurant(id);
            List<DishViewModel> dishesVm = new List<DishViewModel>(); foreach (var d in dishes)
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
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult RemoveToCart(int id)
        {
            // Removes 
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Checkout(DishViewModel[] dishes)
        {
            return View();
        }
    }
}

