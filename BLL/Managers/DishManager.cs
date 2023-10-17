using System.Collections.Generic;
using BLL.BusinessExceptions;
using BLL.Interfaces;
using DataAccessLayer.DBAccesses;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL.Managers
{
    /// <summary>
    /// Manager to create, change and get dishes
    /// </summary>
    public class DishManager:IDishManager
    {
        private DishesDB DishesDb { get; }
        private IConfiguration Configuration { get; }
        /// <summary>
        /// The manager constructor
        /// </summary>
        /// <param name="configuration">The configuration used to inject the manager</param>
        public DishManager(IConfiguration configuration)
        {
            Configuration = configuration;
            DishesDb = new DishesDB(configuration);
        }

        /// <summary>
        /// Method to add a dish to a restaurant's menu
        /// </summary>
        /// <param name="idRestaurant">The id of the restaurant that offers the dish</param>
        /// <param name="name">The name of the dish</param>
        /// <param name="price">The price in cents of the dish</param>
        public void AddDish(int idRestaurant, string name, int price)
        {
            //result is the number of rows affected, so if it is 0 then the dish was not added
            int result = DishesDb.AddDish(idRestaurant, name, price);
            if (result == 0)
            {
                throw new DataBaseException("Error occurred, dish " + name + " was not added");
            }
        }

        /// <summary>
        /// Method to set a dish availability
        /// </summary>
        /// <param name="idDish">The primary key of the dish affected</param>
        /// <param name="isAvailable">The value given of the availability attributes</param>
        public void SetAvailability(int idDish, bool isAvailable)
        {
            //result is the number of rows affected, so if it is 0 then the dish was not added
            int result = DishesDb.SetAvailability(idDish, isAvailable);
            if (result == 0)
            {
                throw new DataBaseException("Error occurred, dish " + idDish + "  availability has not been set.");
            }

        }

        /// <summary>
        /// Method to change a dish price
        /// </summary>
        /// <param name="idDish">The primary key of the affected dish</param>
        /// <param name="price">The new price give to the dish</param>
        public void SetPrice(int idDish, int price)
        {

            //result is the number of rows affected, so if it is 0 then the dish was not added
            int result = DishesDb.SetPrice(idDish, price);
            if (result == 0)
            {
                throw new DataBaseException("Error occurred, dish " + idDish + "  price has not been set.");
            }

        }
         /// <summary>
         /// Method to get all dishes from a restaurant
         /// </summary>
         /// <param name="idRestaurant">The primary key of the restaurant</param>
         /// <returns>Returns a list of Dish object</returns>
        public List<Dish> GetAllDishesByRestaurant(int idRestaurant)
        {
            return DishesDb.GetAllDishesByRestaurant(idRestaurant);
        }

         /// <summary>
         /// Method to get a dish using its primary key in the database
         /// </summary>
         /// <param name="idDish">The primary key of the dish</param>
         /// <returns>Returns a Dish object</returns>
        public Dish GetDishById(int idDish)
        {
            return DishesDb.GetDishById(idDish);
        }
    }
}
