using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.BusinessExceptions;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.CompilerServices;
using WebApp.Models;
namespace WebApp.Controllers
{
    public class CustomerController : Controller
    {
        public ICustomerManager CustomerManager { get; }
        public IRestaurantManager RestaurantManager { get; }
        public IOrderManager OrderManager { get; }
        public IDeliveryAreaManager DeliveryAreaManager { get; }
        public IDishManager DishManager { get; }
        public IComposeManager ComposeManager { get; }
        public CustomerController(ICustomerManager customerManager, IRestaurantManager restaurantManager,
        IOrderManager orderManager, IDeliveryAreaManager deliveryAreaManager, IDishManager dishManager, IComposeManager composeManager)
        {
            CustomerManager = customerManager;
            RestaurantManager = restaurantManager;
            OrderManager = orderManager;
            DeliveryAreaManager = deliveryAreaManager;
            DishManager = dishManager;
            ComposeManager = composeManager;
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

            List<Restaurant> restaurants = RestaurantManager.GetAllRestaurants();
            List<RestaurantViewModel> restaurantsVm = new List<RestaurantViewModel>();
            foreach (var r in restaurants)
            {
                string deliveryAreaName = DeliveryAreaManager.GetDeliveryAreaById(r.IdArea).Name;
                restaurantsVm.Add(new RestaurantViewModel()
                {
                    IdRestaurant = r.IdRestaurant,
                    AreaName = deliveryAreaName,
                    ImagePath = r.Image,
                    IconPath = r.Logo,
                    Name = r.Name
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

            HttpContext.Session.SetInt32("CurrentRestaurantId", id);
            Restaurant r = RestaurantManager.GetRestaurantById(id);
            List<Dish> dishes = DishManager.GetAllDishesByRestaurant(id);
            List<CompositionViewModel> compositionVm = new List<CompositionViewModel>();
            foreach (var d in dishes)
            {
                if (d.IsAvailable)
                {
                    compositionVm.Add(new CompositionViewModel()
                    {
                        DishImagePath = d.Image,
                        DishName = d.Name,
                        DishPrice = (double)d.Price / 100,
                        Quantity = 0,
                        IdDish = d.IdDish
                    });
                }
            }
            string areaName = DeliveryAreaManager.GetDeliveryAreaById(r.IdArea).Name;
            OrderViewModel orderViewModel = new OrderViewModel()
            {
                RestaurantName = r.Name,
                AreaName = areaName,
                AvailableCompositions = compositionVm,
                ImagePath = r.Image
            };
            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult Checkout(OrderViewModel orderViewModel)
        {
            if (HttpContext.Session.GetString("TypeOfUser") != "Customer" || HttpContext.Session.GetInt32("IdMember") == null)
            {
                return RedirectToAction("Login", "Home");
            }

            //initializing orderViewModel null variables
            orderViewModel.OrderCompositions = new List<CompositionViewModel>();
            orderViewModel.OrderTotal = 0;

            // Recreate the dishes available
            int cpt = 0;
            foreach (var d in DishManager.GetAllDishesByRestaurant((int)HttpContext.Session.GetInt32("CurrentRestaurantId")))
            {
                if (d.IsAvailable)
                {
                    orderViewModel.AvailableCompositions[cpt].DishImagePath = d.Image;
                    orderViewModel.AvailableCompositions[cpt].DishName = d.Name;
                    orderViewModel.AvailableCompositions[cpt].DishPrice = d.Price;
                    orderViewModel.AvailableCompositions[cpt].IdDish = d.IdDish;
                    cpt++;
                }
            }
            // Adds the quantity to available dishes
            int j = 0;
            for (int i = 0; i < orderViewModel.AvailableCompositions.Count; i++)
            {
                CompositionViewModel cTemp = orderViewModel.AvailableCompositions[i];
                if (cTemp.Quantity > 0)
                {
                    orderViewModel.OrderCompositions.Add(new CompositionViewModel()
                    {
                        IdDish = cTemp.IdDish,
                        Quantity = cTemp.Quantity,
                        DishPrice = cTemp.DishPrice,
                        DishName = cTemp.DishName,
                        DishImagePath = cTemp.DishImagePath
                    });
                    orderViewModel.OrderTotal += cTemp.Quantity * cTemp.DishPrice;
                    orderViewModel.OrderCompositions[j].DishPrice /= 100;
                    ++j;
                }
            }

            //Empty orders can't placed
            if (orderViewModel.OrderCompositions.Count == 0)
            {
                ModelState.AddModelError("", "Please first choose something to eat");
                return RedirectToAction("Order", new
                {
                    id = (int)HttpContext.Session.GetInt32("CurrentRestaurantId")
                });
            }

            Customer c = CustomerManager.GetCustomerById((int)HttpContext.Session.GetInt32("IdMember"));
            orderViewModel.CustomerLastName = c.LastName;
            orderViewModel.CustomerFirstName = c.FirstName;
            orderViewModel.RestaurantName = RestaurantManager.GetRestaurantById((int)HttpContext.Session.GetInt32("CurrentRestaurantId")).Name;
            orderViewModel.OrderTotal = (double)orderViewModel.OrderTotal / 100;

            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult Confirmation(OrderViewModel orderViewModel)
        {
            if (HttpContext.Session.GetString("TypeOfUser") != "Customer" || HttpContext.Session.GetInt32("IdMember") == null)
            {
                return RedirectToAction("Login", "Home");
            }

            foreach (CompositionViewModel comp in orderViewModel.OrderCompositions)
            {
                Dish d = DishManager.GetDishById(comp.IdDish);
                comp.DishPrice = (double)d.Price / 100;
                comp.DishName = d.Name;
                comp.DishImagePath = d.Image;
            }

            Customer c = CustomerManager.GetCustomerById((int)HttpContext.Session.GetInt32("IdMember"));
            orderViewModel.CustomerLastName = c.LastName;
            orderViewModel.CustomerFirstName = c.FirstName;
            orderViewModel.RestaurantName = RestaurantManager.GetRestaurantById((int)HttpContext.Session.GetInt32("CurrentRestaurantId")).Name;

            // Model validation
            DeliveryArea delA = DeliveryAreaManager.GetDeliveryAreaByPostcode(orderViewModel.PostCode);
            if (delA == null)
            {
                ModelState.AddModelError("", "Can't deliver in this area. Try a different postcode.");
                return View("Checkout", orderViewModel);
            }
            if (orderViewModel.ExpectedDeliveryTime.Equals(DateTime.MinValue))
            {
                ModelState.AddModelError("", "Choose a delivery time.");
                return View("Checkout", orderViewModel); ;
            }
            if (orderViewModel.DeliveryAddress == null)
            {
                ModelState.AddModelError("", "Specify the delivery address");
                return View("Checkout", orderViewModel);
            }
            orderViewModel.AreaName = delA.Name;
            // Inserts the compositions in the DB and creates the order
            int idOrder=-1;
            try
            {
                idOrder = OrderManager.CreateNewOrder((int)HttpContext.Session.GetInt32("IdMember"), delA.IdArea,
                    orderViewModel.ExpectedDeliveryTime, orderViewModel.DeliveryAddress);
            }
            catch (BusinessRuleException e)
            {
                ModelState.AddModelError("", "No courier available in this area.");
                return View("Checkout", orderViewModel);
            }

            foreach (var comp in orderViewModel.OrderCompositions)
            {
                ComposeManager.AddComposition(comp.IdDish, idOrder, comp.Quantity);
            }
            OrderManager.SetTotal(idOrder);

            return View(orderViewModel);
        }

        public IActionResult MyOrders()
        {
            int? idCustomer = HttpContext.Session.GetInt32("IdMember");
            if (HttpContext.Session.GetString("TypeOfUser") != "Customer" || idCustomer == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<Order> orders = OrderManager.GetAllOrdersByCustomer(((int)idCustomer));
            List<OrderViewModel> ordersVm = new List<OrderViewModel>(orders.Count);
            foreach (var o in orders)
            {
                ordersVm.Add(new OrderViewModel()
                {
                    AreaName = DeliveryAreaManager.GetDeliveryAreaById(o.IdArea).Name,
                    OrderTotal = (double)o.OrderTotal / 100,
                    RestaurantName = RestaurantManager.GetRestaurantByOrder(o.IdOrder).Name,
                    DeliveryAddress = o.DeliveryAddress,
                    IdOrder = o.IdOrder,
                    TimeOfDelivery = o.TimeOfDelivery
                });
            }
            return View(ordersVm);
        }

        public IActionResult Details(int id)
        {
            int? idCustomer = HttpContext.Session.GetInt32("IdMember");
            if (HttpContext.Session.GetString("TypeOfUser") != "Customer" || idCustomer == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<Composition> compositions = ComposeManager.GetCompositionsByOrder(id);
            List<CompositionViewModel> orderCompositions = new List<CompositionViewModel>(compositions.Count);
            foreach (var c in compositions)
            {
                //retrieving corresponding dish
                Dish d = DishManager.GetDishById(c.ID_Dish);

                //mapping compositions and dish infos to compostitionViewModel
                orderCompositions.Add(new CompositionViewModel()
                {
                    DishImagePath = d.Image,
                    DishName = d.Name,
                    DishPrice = (double)d.Price / 100,
                    IdDish = d.IdDish,
                    Quantity = c.Quantity
                });

            }

            Order o = OrderManager.GetOrderById(id);
            Customer cus = CustomerManager.GetCustomerById(o.IdCustomer);
            OrderViewModel orderVm = new OrderViewModel()
            {
                AreaName = DeliveryAreaManager.GetDeliveryAreaById(o.IdArea).Name,
                OrderTotal = (double)o.OrderTotal / 100,
                RestaurantName = RestaurantManager.GetRestaurantByOrder(o.IdOrder).Name,
                DeliveryAddress = o.DeliveryAddress,
                IdOrder = o.IdOrder,
                TimeOfDelivery = o.TimeOfDelivery,
                OrderCompositions = orderCompositions,
                CustomerLastName = cus.LastName,
                CustomerFirstName = cus.FirstName,
                ExpectedDeliveryTime = o.ExpectedDeliveryTime
            };

            return View(orderVm);
        }

        public IActionResult Cancel(int id)
        {
            try
            {
                OrderManager.DeleteOrder(id);
            }
            catch (BusinessRuleException bre)
            {
                ModelState.AddModelError("", "Hello");
                int thisId = id;
                return RedirectToAction("Details", new
                {
                    id = thisId
                });
            }

            return RedirectToAction("MyOrders");
        }
    }
}

