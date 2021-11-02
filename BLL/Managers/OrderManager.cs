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

        public OrderManager()
        {
            OrdersDb = new OrdersDB();
            CustomersDb = new CustomersDB();
            CouriersDb = new CouriersDB();
            DeliveryAreasDb = new DeliveryAreasDB();
            CompositionDb = new CompositionDB();
        }

        public void CreateNewOrder(int idCustomer, int idCourier, int idArea, DateTime expectedDeliveryTime, string deliveryAddress)
        {
            //result is the number of rows affected, so if it is 0 then the order was not added
            int result = OrdersDb.AddOrder(idCustomer, idCourier, idArea, expectedDeliveryTime, deliveryAddress);
            if (result == 0)
            {
                throw new DataBaseException("Error occurred, order was not created.");
            }
        }

        public void DeleteOrder(int idOrder)
        {
            CompositionDb.DeleteCompositionByOrder(idOrder);
            OrdersDb.DeleteOrder(idOrder);
        }

        public void SetOrderToDelivered(int idOrder)
        {
            //check that the order has not already been delivered
            Order order = OrdersDb.GetOrderById(idOrder);
            if (order.TimeOfDelivery!= null)
            {
                throw new BusinessRuleException("Order has already been set to delivered");
            }

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

        public int SetTotal(int idOrder, int total)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(int idOrder)
        {
            return OrdersDb.GetOrderById(idOrder);
        }
    }
}