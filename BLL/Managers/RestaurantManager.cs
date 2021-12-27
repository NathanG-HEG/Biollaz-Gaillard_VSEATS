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
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class RestaurantManager : IRestaurantManager

    {
        private RestaurantsDB RestaurantsDb { get; }
        private IConfiguration Configuration { get; }
        private Utilities Utilities { get; }

        public RestaurantManager(IConfiguration configuration)
        {
            Configuration = configuration;
            Utilities = new Utilities(Configuration);
            RestaurantsDb = new RestaurantsDB(Configuration);
        }

        public void CreateRestaurant(int idArea, string name, string emailAddress, string password)
        {

            // Checks for syntax errors
            if (!Utilities.IsEmailAddressCorrect(emailAddress))
                throw new InputSyntaxException(emailAddress + " is not valid");
            if (!Utilities.IsPasswordSyntaxCorrect(password))
                throw new InputSyntaxException("Password must contain at least 8 characters, a number and a capital");

            //check if email address is redundant
            if (Utilities.IsEmailAddressInDatabase(emailAddress))
                throw new BusinessRuleException("An account using this email address already exists");

            int result = RestaurantsDb.AddRestaurant(idArea, name, emailAddress, password);
            if (result == 0)
            {
                throw new DataBaseException("Error occurred, restaurant " + name + " was not created.");
            }
        }

        public void UpdateImage(string path, int idRestaurant)
        {
            //creating a file instance to check its existence
            var image = new FileInfo(path);

            //update the DB if the path is valid
            if (image.Exists)
            {
                if (RestaurantsDb.UpdateImage(path, idRestaurant) != 0)
                {
                    throw new DataBaseException("File could not be found");
                }
            }

        }

        public void UpdateLogo(string path, int idRestaurant)
        {
            //creating a file instance to check its existence
            var image = new FileInfo(path);

            //update the DB if the path is valid
            if (image.Exists)
            {
                if (RestaurantsDb.UpdateLogo(path, idRestaurant) != 0)
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

        public Restaurant GetRestaurantById(int id)
        {
            return RestaurantsDb.GetRestaurantById(id);
        }

        public Restaurant GetRestaurantByOrder(int idOrder)
        {
            return RestaurantsDb.GetRestaurantByOrder(idOrder);
        }
    }
}
