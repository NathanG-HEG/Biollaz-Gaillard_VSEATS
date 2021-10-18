using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Interfaces
{
    public interface IOrdersDB
    {
        public int AddOrder(int idCustomer, int idCourier, int idArea, DateTime ExpectedDeliveryTime, string deliveryAddress);
        public int SetOrderToDelivered(int idOrder);
        public List<Order> GetAllOrdersByCustomer(int idCustomer);
        public List<Order> GetAllOrdersByCourier(int idCourier);
        public List<Order> GetAllOrdersByRestaurant(int idRestaurant);
        public int SetTotal(int total);


    }
}
