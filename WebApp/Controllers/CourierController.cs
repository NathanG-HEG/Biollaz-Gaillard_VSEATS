using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BLL.Interfaces;
using WebApp.Models;
using DTO;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller that has all the functions available for the courier.
    /// </summary>
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

        /// <summary>
        /// Calls the index view when a courier is signed in. The index page is a list of the courier's orders.
        /// </summary>
        /// <returns>Index view</returns>
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

        /// <summary>
        /// Sets an order to deliver by calling the order manager. Then reload the page to display the changes.
        /// </summary>
        /// <param name="id">Order's primary key</param>
        /// <returns>The current view</returns>
        public IActionResult SetToDelivered(int id)
        {
            OrderManager.SetOrderToDelivered(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        /// <summary>
        /// Display more detailed information about a given order.
        /// </summary>
        /// <param name="id">Order's primary key</param>
        /// <returns>The details view</returns>
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
            //list of the ordered compositions
            List<CompositionViewModel> orderCompositions = null;
            if (compositions != null)
            {
                orderCompositions = new List<CompositionViewModel>(compositions.Count);
                foreach (var c in compositions)
                {
                    //retrieving corresponding dish
                    Dish d = DishManager.GetDishById(c.ID_Dish);

                    //mapping compositions and dish infos to compostitionViewModel
                    orderCompositions.Add(new CompositionViewModel()
                    {
                        DishImagePath = d.Image,
                        DishName = d.Name,
                        DishPrice = (double) d.Price / 100,
                        IdDish = d.IdDish,
                        Quantity = c.Quantity
                    });

                }
            }

            OrderViewModel orderViewModel = new OrderViewModel()
            {
                IdOrder = o.IdOrder,
                CustomerFirstName = customerFirstname,
                CustomerLastName = customerLastName,
                DeliveryAddress = o.DeliveryAddress,
                AreaName = postcode + " " + areaName,
                OrderCompositions = orderCompositions,
                ExpectedDeliveryTime = o.ExpectedDeliveryTime,
                TimeOfDelivery = o.TimeOfDelivery,
                OrderTotal = o.OrderTotal

            };
            return View(orderViewModel);
        }
    }
}
