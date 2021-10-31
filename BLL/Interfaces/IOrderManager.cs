using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BLL.Interfaces
{
    public interface IOrderManager
    {
        public void CreateNewOrder(int idCustomer, int idCourier, int idArea, DateTime ExpectedDeliveryTime, string deliveryAddress);
        public void SetOrderToDelivered(int idOrder);
        public List<Order> GetAllOrdersByCustomer(int idCustomer);
        public List<Order> GetAllOrdersByCourier(int idCourier);
        public List<Order> GetAllOrdersByRestaurant(int idRestaurant);
        public int SetTotal(int idOrder, int total);
        public Order GetOrderById(int idOrder);

    }
}
