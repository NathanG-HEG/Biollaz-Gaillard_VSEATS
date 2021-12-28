using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Http;
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
            if (!HttpContext.Session.GetString("TypeOfUser").Equals("Restaurant"))
            {
                return RedirectToAction("Login", "Home");
            }

            Restaurant r = RestaurantManager.GetRestaurantById((int) HttpContext.Session.GetInt32("IdMember"));
            RestaurantViewModel rvm = new RestaurantViewModel()
            {
                AreaName = DeliveryAreaManager.GetDeliveryAreaById(r.IdArea).Name,
                IconPath = r.Logo,
                Name = r.Name
            };
            List<Order> orders = OrderManager.GetAllOrdersByRestaurant((int) HttpContext.Session.GetInt32("IdMember"));
            foreach (var o in orders)
            {
                rvm.Revenue += o.OrderTotal;
            }

            rvm.Revenue /= 100;
            rvm.Sales = orders.Count;

            return View(rvm);
        }


    }
}
