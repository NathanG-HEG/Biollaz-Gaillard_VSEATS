using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Interfaces
{
    public interface IOrdersDB
    {
        public int AddOrder(int idCustomer, int idCourier, int idArea, List<Dish> dishes, string deliveryAddress);
        public void SetOrderToDelivered(int idOrder);
        public List<Order> GetAllOrdersByCustomer(int idCustomer);
        public List<Order> GetAllOrdersByCourier(int idCourier);
        public List<Order> GetAllOrdersByRestaurant(int idRestaurant);


    }
}
