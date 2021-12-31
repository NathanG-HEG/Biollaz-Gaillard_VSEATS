using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRestaurantsDB
    {
        public int AddRestaurant(int idArea, string name, string emailAddress, string pwdHash, string salt);
        public Restaurant GetRestaurantByName(string name);
        public Restaurant GetRestaurantById(int id);
        public Restaurant GetRestaurantByLogin(string emailAddress, string password);
        public Restaurant GetRestaurantByOrder(int idOrder);
        public List<Restaurant> GetAllRestaurants();
        public List<Restaurant> GetAllRestaurantsByArea(int idArea);
        public int UpdateImage(string path, int idRestaurant);
        public int UpdateLogo(string path, int idRestaurant);

    }
}
