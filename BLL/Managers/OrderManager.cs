﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.BusinessExceptions;
using BLL.Interfaces;
using DataAccessLayer;
using DataAccessLayer.DBAccesses;

namespace BLL
{
    public class OrderManager:IOrderManager
    {
        private OrdersDB OrdersDb { get; }
        private CustomersDB CustomersDb { get; }
        private CouriersDB CouriersDb { get; }
        private DeliveryAreasDB DeliveryAreasDb { get; }
        private CompositionDB CompositionDb { get; }
        private DishesDB DishesDb { get; }

        public OrderManager()
        {
            OrdersDb = new OrdersDB();
            CustomersDb = new CustomersDB();
            CouriersDb = new CouriersDB();
            DeliveryAreasDb = new DeliveryAreasDB();
            CompositionDb = new CompositionDB();
            DishesDb = new DishesDB();
        }

        public void CreateNewOrder(int idCustomer, int idArea, DateTime expectedDeliveryTime, string deliveryAddress)
        {
            // Finds an available courier in the area
            int idCourier=-1;
            List <Courier> availableCouriers = CouriersDb.GetAllCouriersByArea(idArea);

            if (availableCouriers == null)
            {
                throw new BusinessRuleException("No courier registered at area " + idArea);
            }

            foreach (var c in availableCouriers)
            {
                if (c.numberOfCurrentOrders < Utilities.MaxOrdersSimultaneously)
                {
                    idCourier = c.IdCourier;
                    break;
                }
            }
            // if no courier were found, throw businessRuleException
            if (idCourier == -1)
            {
                throw new BusinessRuleException("No courier available at area: " + idArea);
            }
            //result is the number of rows affected, so if it is 0 then the order was not added
            int result = OrdersDb.AddOrder(idCustomer, idCourier, idArea, expectedDeliveryTime, deliveryAddress);
            if (result == 0)
            {
                throw new DataBaseException("Error occurred, order was not created.");
            }
        }

        public void DeleteOrder(int idOrder)
        {
            //compositions related to the order must be deleted first to respect referential integrity 
            CompositionDb.DeleteCompositionByOrder(idOrder);
            OrdersDb.DeleteOrder(idOrder);
        }

        public void SetOrderToDelivered(int idOrder)
        {
            
            //result is the number of rows affected, so if it is 0 then the status was not updated
            int result = OrdersDb.SetOrderToDelivered(idOrder);
            if (result == 0)
            {
                throw new DataBaseException("Error occurred, delivery time of order " + idOrder + " has not been set.");
            }
        }

        public List<Order> GetAllOrdersByCustomer(int idCustomer)
        {
            return OrdersDb.GetAllOrdersByCustomer(idCustomer);
        }

        public List<Order> GetAllOrdersByCourier(int idCourier)
        {
            return OrdersDb.GetAllOrdersByCourier(idCourier);
        }

        public List<Order> GetAllOrdersByRestaurant(int idRestaurant)
        {
            return OrdersDb.GetAllOrdersByRestaurant(idRestaurant);
        }

        public void SetTotal(int idOrder)
        {
            List<Composition> compositions = CompositionDb.GetCompositionsByOrder(idOrder);

            int total = 0;
            foreach (var c in compositions)
            {
                Dish dish = DishesDb.GetDishById(c.ID_Dish);
                total += dish.Price * c.Quantity;
            }
            OrdersDb.SetTotal(idOrder, total); 
        }

        public Order GetOrderById(int idOrder)
        {
            return OrdersDb.GetOrderById(idOrder);
        }
    }
}
