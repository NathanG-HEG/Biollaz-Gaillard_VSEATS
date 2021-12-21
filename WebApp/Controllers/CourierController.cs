using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.Interfaces;
using DataAccessLayer;
using WebApp.Models;
using DTO;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    public class CourierController : Controller
    {
        public IOrderManager OrderManager { get; }
        public ICourierManager CourierManager { get; }
        public ICustomerManager CustomerManager { get; }
        public IDeliveryAreaManager DeliveryAreaManager { get; }
        public IComposeManager ComposeManager { get; }
        public IDishManager DishManager { get; }

        public CourierController(IOrderManager orderManager, ICourierManager courierManager, ICustomerManager customerManager,
                                    IDeliveryAreaManager deliveryAreaManager, IComposeManager composeManager, IDishManager dishManager)
        {
            OrderManager = orderManager;
            CourierManager = courierManager;
            CustomerManager = customerManager;
            DeliveryAreaManager = deliveryAreaManager;
            ComposeManager = composeManager;
            DishManager = dishManager;
        }

        [HttpGet]
        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("TypeOfUser") != "Courier")
            {
                return RedirectToAction("Login", "Home");
            }

            int id = (int)HttpContext.Session.GetInt32("IdMember");

            List<Order> orders = OrderManager.GetAllOrdersByCourier(id);
            List<OrderViewModel> ordersViewModel = null;
            if (orders != null)
            {
                ordersViewModel = new List<OrderViewModel>();
                foreach (var o in orders)
                {
                    DeliveryArea deliveryArea = DeliveryAreaManager.GetDeliveryAreaById(o.IdArea);
                    string areaName = deliveryArea.Name;
                    int postcode = deliveryArea.Postcode;

                    string customerLastName = CustomerManager.GetCustomerById(o.IdCustomer).LastName;

                    ordersViewModel.Add(new OrderViewModel()
                    {
                        IdOrder = o.IdOrder,
                        DeliveryAddress = o.DeliveryAddress,
                        CustomerLastName = customerLastName,
                        ExpectedDeliveryTime = o.ExpectedDeliveryTime,
                        TimeOfDelivery = o.TimeOfDelivery,
                        AreaName = postcode + " " + areaName
                    });
                }
            }

            return View(ordersViewModel);
        }

        public IActionResult SetToDelivered(int id)
        {
            OrderManager.SetOrderToDelivered(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Details(int id)
        {
            Order o = OrderManager.GetOrderById(id);

            DeliveryArea deliveryArea = DeliveryAreaManager.GetDeliveryAreaById(o.IdArea);
            string areaName = deliveryArea.Name;
            int postcode = deliveryArea.Postcode;

            Customer customer = CustomerManager.GetCustomerById(o.IdCustomer);
            string customerFirstname = customer.FirstName;
            string customerLastName = customer.LastName;

            List<Composition> compositions = ComposeManager.GetCompositionsByOrder(id);

            //list of the ordered dishes
            //used to display infos about dishes
            List<DishViewModel> dishes = null;
            if (compositions != null)
            {
                dishes = new List<DishViewModel>(compositions.Count);
                int cpt = 0;
                foreach (var c in compositions)
                {
                    Dish d = DishManager.GetDishById(compositions[cpt].ID_Dish);
                    dishes.Add(new DishViewModel() { Image = d.Image, IdRestaurant = d.IdRestaurant, Name = d.Name, Price = (double)d.Price / 100 });
                    cpt++;
                }
            }

            OrderViewModel orderViewModel = new OrderViewModel()
            {
                IdOrder = o.IdOrder,
                CustomerFistName = customerFirstname,
                CustomerLastName = customerLastName,
                DeliveryAddress = o.DeliveryAddress,
                AreaName = postcode + " " + areaName,
                OrderCompositions = compositions,
                AvailableDishes = dishes,
                ExpectedDeliveryTime = o.ExpectedDeliveryTime,
                TimeOfDelivery = o.TimeOfDelivery,
                OrderTotal = o.OrderTotal

            };
            return View(orderViewModel);
        }
    }
}
