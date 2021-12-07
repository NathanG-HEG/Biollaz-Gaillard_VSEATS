using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;


namespace DataAccessLayer.Interfaces
{
    public interface IOrdersDB
    {
        public int AddOrder(int idCustomer, int idCourier, int idArea, DateTime ExpectedDeliveryTime, string deliveryAddress);
        public int SetOrderToDelivered(int idOrder);
        public int SetOrderToUnDelivered(int idOrder);
        public List<Order> GetAllOrdersByCustomer(int idCustomer);
        public List<Order> GetAllOrdersByCourier(int idCourier);
        public List<Order> GetAllOrdersByRestaurant(int idRestaurant);
        public int SetTotal(int order, int total);
        public Order GetOrderById(int idOrder);
        public int DeleteOrder(int idOrder);


    }
}
