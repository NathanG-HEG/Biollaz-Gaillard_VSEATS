using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
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
                    orderViewModel.OrderCompositions[i].DishPrice /= 100;
                }
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
                comp.DishPrice = d.Price;
                comp.DishName = d.Name;
                comp.DishImagePath = d.Image;
            }

            Customer c = CustomerManager.GetCustomerById((int)HttpContext.Session.GetInt32("IdMember"));
            orderViewModel.CustomerLastName = c.LastName;
            orderViewModel.CustomerFirstName = c.FirstName;
            orderViewModel.RestaurantName = RestaurantManager.GetRestaurantById((int)HttpContext.Session.GetInt32("CurrentRestaurantId")).Name;
            DeliveryArea delA = DeliveryAreaManager.GetDeliveryAreaByPostcode(orderViewModel.PostCode);
            // Model validation
            if (delA == null)
            {
                ModelState.AddModelError("", "Can't deliver in this area. Try a different postcode.");
                return Redirect(Request.Headers["Referer"].ToString());
            }
            if (orderViewModel.ExpectedDeliveryTime.Equals(DateTime.MinValue))
            {
                ModelState.AddModelError("","Choose a delivery time.");
                return Redirect(Request.Headers["Referer"].ToString());
            }
            if (orderViewModel.DeliveryAddress == null)
            {
                ModelState.AddModelError("","Specify the delivery address");
                return Redirect(Request.Headers["Referer"].ToString());
            }

            orderViewModel.AreaName = delA.Name;
            return View(orderViewModel);
        }
    }
}

