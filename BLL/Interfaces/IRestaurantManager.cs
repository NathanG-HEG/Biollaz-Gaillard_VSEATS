using System.Collections.Generic;
using DTO;

namespace BLL.Interfaces
{
    public interface IRestaurantManager
    {
        public void CreateRestaurant(int idArea, string name, string emailAddress, string password);
        public Restaurant GetRestaurantByLogin(string email, string password);
        public Restaurant GetRestaurantById(int id);
        public List<Restaurant> GetAllRestaurants();
        public Restaurant GetRestaurantByOrder(int idOrder);

    }
}
