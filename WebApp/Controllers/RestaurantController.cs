using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller that has all the function a logged in restaurant has.
    /// </summary>
    public class RestaurantController : Controller
    {
        private IRestaurantManager RestaurantManager { get; }
        private IOrderManager OrderManager { get; }
        private IDeliveryAreaManager DeliveryAreaManager { get; }
        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="restaurantManager">The restaurant manager object</param>
        /// <param name="orderManager">The order manager object</param>
        /// <param name="deliveryAreaManager">The delivery area manager</param>
        public RestaurantController(IRestaurantManager restaurantManager, IOrderManager orderManager, IDeliveryAreaManager deliveryAreaManager)
        {
            RestaurantManager = restaurantManager;
            OrderManager = orderManager;
            DeliveryAreaManager = deliveryAreaManager;
        }

        /// <summary>
        /// Default page when logged in as a restaurant
        /// </summary>
        /// <returns>Create a an HTML page corresponding to a sales dashboard</returns>
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
            if (orders != null)
            {
                foreach (var o in orders)
                {
                    rvm.Revenue += o.OrderTotal;
                }

                rvm.Revenue /= 100;
                rvm.Sales = orders.Count;
            }
            else
            {
                rvm.Revenue = 0;
                rvm.Sales = 0;
            }

            return View(rvm);
        }


    }
}
