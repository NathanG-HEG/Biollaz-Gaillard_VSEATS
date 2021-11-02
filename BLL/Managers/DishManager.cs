using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.BusinessExceptions;
using BLL.Interfaces;
using DataAccessLayer.DBAccesses;

namespace BLL
{
    public class DishManager:IDishManager
    {
        private DishesDB DishesDb { get; }
        private CompositionDB CompositionDb { get; }
        private RestaurantsDB RestaurantsDb { get; }

        public DishManager()
        {
            DishesDb = new DishesDB();
            CompositionDb = new CompositionDB();
            RestaurantsDb = new RestaurantsDB();
        }

        public void AddDish(int idRestaurant, string name, int price)
        {
            //result is the number of rows affected, so if it is 0 then the dish was not added
            int result = DishesDb.AddDish(idRestaurant, name, price);
            if (result == 0)
            {
                throw new DataBaseException("Error occurred, dish " + name + " was not added");
            }
        }

        public void SetAvailability(int idDish, bool isAvailable)
        {
            //result is the number of rows affected, so if it is 0 then the dish was not added
            int result = DishesDb.SetAvailability(idDish, isAvailable);
            if (result == 0)
            {
                throw new DataBaseException("Error occurred, dish " + idDish + "  availability has not been set.");
            }

        }

        public void SetPrice(int idDish, int price)
        {

            //result is the number of rows affected, so if it is 0 then the dish was not added
            int result = DishesDb.SetPrice(idDish, price);
            if (result == 0)
            {
                throw new DataBaseException("Error occurred, dish " + idDish + "  price has not been set.");
            }

        }

        public List<Dish> GetAllDishesByRestaurant(int idRestaurant)
        {
            return DishesDb.GetAllDishesByRestaurant(idRestaurant);
        }

        public Dish GetDishById(int idDish)
        {
            return DishesDb.GetDishById(idDish);
        }
    }
}
