using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.BusinessExceptions;
using BLL.Interfaces;
using DataAccessLayer.DBAccesses;

namespace BLL
{
    public class RestaurantManager:IRestaurantManager

    {
    private RestaurantsDB RestaurantsDb { get; }
    private DishesDB DishesDb { get; }
    private DeliveryAreasDB DeliveryAreasDb { get; }

    public RestaurantManager()
    {
        RestaurantsDb = new RestaurantsDB();
        DishesDb = new DishesDB();
        DeliveryAreasDb = new DeliveryAreasDB();
    }

        public void CreateRestaurant(int idArea, string name, string emailAddress, string password)
        {
            RestaurantsDb.AddRestaurant(idArea, name, emailAddress, password);
        }

        public void UpdateImage(string path)
        {
            //creating a file instance to check its existence
            var image = new FileInfo(path);

            //update the DB if the path is valid
            if (image.Exists)
            {
                if (RestaurantsDb.UpdateImage(path)!=0)
                {
                    throw new DataBaseException("File could not be found");
                }
            }

        }

        public void UpdateLogo(string path)
        {
            //creating a file instance to check its existence
            var image = new FileInfo(path);

            //update the DB if the path is valid
            if (image.Exists)
            {
                if (RestaurantsDb.UpdateLogo(path) != 0)
                {
                    throw new DataBaseException("File could not be found");
                }
            }
        }

        public Restaurant GetRestaurantByLogin(string email, string password)
        {
            return RestaurantsDb.GetRestaurantByLogin(email, password);
        }

        public Restaurant GetRestaurantByName(string name)
        {
            return RestaurantsDb.GetRestaurantByName(name);
        }

        public List<Restaurant> GetAllRestaurantsArea(int idArea)
        {
            return RestaurantsDb.GetAllRestaurantsByArea(idArea);
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return RestaurantsDb.GetAllRestaurants();
        }
    }
}
