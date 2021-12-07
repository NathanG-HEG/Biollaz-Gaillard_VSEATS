using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public CourierController(IOrderManager orderManager, ICourierManager courierManager, ICustomerManager customerManager,
                                    IDeliveryAreaManager deliveryAreaManager)
        {
            OrderManager = orderManager;
            CourierManager = courierManager;
            CustomerManager = customerManager;
            DeliveryAreaManager = deliveryAreaManager;
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

                Customer cus = CustomerManager.GetCustomerById(orders[0].IdCustomer);
                string customerFirstName = cus.FirstName;
                string customerLastName = cus.LastName;

                ordersViewModel.Add(new OrderViewModel()
                {
                    IdOrder = o.IdOrder,
                    DeliveryAddress = o.DeliveryAddress,
                    CustomerFistName = customerFirstName,
                    CustomerLastName = customerLastName,
                    CourierLastName = courierLastName,
                    ExpectedDeliveryTime = o.ExpectedDeliveryTime,
                    TimeOfDelivery = o.TimeOfDelivery
                });
            }

            return View(ordersViewModel);
        }

        [HttpGet]
        public IActionResult SetToDelivered(int id)
        {
            OrderManager.SetOrderToDelivered(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
