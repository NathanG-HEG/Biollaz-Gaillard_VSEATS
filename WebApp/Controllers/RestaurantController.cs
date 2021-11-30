using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;

namespace WebApp.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurantManager RestaurantManager { get; }
        private IOrderManager OrderManager { get; }
        private IDeliveryAreaManager DeliveryAreaManager { get; }
        public RestaurantController(IRestaurantManager restaurantManager, IOrderManager orderManager, IDeliveryAreaManager deliveryAreaManager)
        {
            RestaurantManager = restaurantManager;
            OrderManager = orderManager;
            DeliveryAreaManager = deliveryAreaManager;
        }

        public IActionResult Index()
        {
            List<Restaurant> restaurants = RestaurantManager.GetAllRestaurants();
            return View(restaurants);
        }

    }
}
