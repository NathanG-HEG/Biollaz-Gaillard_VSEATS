using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using WebApp.Models;

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
            List<RestaurantViewModel> restaurantsVM = new List<RestaurantViewModel>();

            foreach (var r  in restaurants)
            {
               string deliveryAreaName = DeliveryAreaManager.GetDeliveryAreaById(r.IdArea).Name;
                restaurantsVM.Add(new RestaurantViewModel(){AreaName = deliveryAreaName,IconPath = r.Logo, Name = r.Name});
            }
            return View(restaurantsVM);
        }


    }
}
