using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void CreateNewOrder(int idCustomer, int idCourier, int idArea, DateTime ExpectedDeliveryTime, string deliveryAddress)
        {
            throw new NotImplementedException();
        }

        public void SetOrderToDelivered(int idOrder)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrdersByCustomer(int idCustomer)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrdersByCourier(int idCourier)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrdersByRestaurant(int idRestaurant)
        {
            throw new NotImplementedException();
        }

        public int SetTotal(int idOrder, int total)
        {
            throw new NotImplementedException();
        }
    }
}
