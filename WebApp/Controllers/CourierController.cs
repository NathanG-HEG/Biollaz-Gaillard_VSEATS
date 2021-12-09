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
namespace WebApp.Controllers
{
    public class CourierController : Controller
    {
        public IOrderManager OrderManager { get; }
        public ICourierManager CourierManager { get; }
        public ICustomerManager CustomerManager { get; }
        public IDeliveryAreaManager DeliveryAreaManager { get; }
        public IComposeManager ComposeManager { get; }

        public CourierController(IOrderManager orderManager, ICourierManager courierManager, ICustomerManager customerManager,
                                    IDeliveryAreaManager deliveryAreaManager, IComposeManager composeManager)
        {
            OrderManager = orderManager;
            CourierManager = courierManager;
            CustomerManager = customerManager;
            DeliveryAreaManager = deliveryAreaManager;
            ComposeManager = composeManager;
        }

        [HttpGet]
        public IActionResult Index(int Id)
        {
            Id = 4;
            List<Order> orders = OrderManager.GetAllOrdersByCourier(Id);

            Courier courier = CourierManager.GetCourierById(orders[0].IdCourier);
            string courierLastName = courier.LastName;

            List<OrderViewModel> ordersViewModel = new List<OrderViewModel>();
            foreach (var o in orders)
            {
                string areaName = DeliveryAreaManager.GetDeliveryAreaById(o.IdArea).Name;

                string customerLastName = CustomerManager.GetCustomerById(o.IdCustomer).LastName;
                
                ordersViewModel.Add(new OrderViewModel()
                {
                    IdOrder = o.IdOrder,
                    DeliveryAddress = o.DeliveryAddress,
                    CustomerLastName = customerLastName,
                    ExpectedDeliveryTime = o.ExpectedDeliveryTime,
                    TimeOfDelivery = o.TimeOfDelivery,
                    AreaName = areaName
                });
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

            string areaName = DeliveryAreaManager.GetDeliveryAreaById(o.IdArea).Name;

            Customer customer = CustomerManager.GetCustomerById(o.IdCustomer);
            string customerFirstname = customer.FirstName;
            string customerLastName = customer.LastName;

            List<Composition> compositions = ComposeManager.GetCompositionsByOrder(id);

            OrderViewModel orderViewModel = new OrderViewModel()
            {
                CustomerFistName = customerFirstname,
                CustomerLastName = customerLastName,
                DeliveryAddress = o.DeliveryAddress,
                AreaName = areaName,
                OrderCompositions = compositions,
                ExpectedDeliveryTime = o.ExpectedDeliveryTime,
                TimeOfDelivery = o.TimeOfDelivery,
                OrderTotal = o.OrderTotal

            };
            return View(orderViewModel);
        }
    }
}
