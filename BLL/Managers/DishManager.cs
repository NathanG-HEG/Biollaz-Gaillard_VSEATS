using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public int AddDish(int idRestaurant, string name, int price)
        {
            throw new NotImplementedException();
        }

        public int SetAvailability(int idDish, bool isAvailable)
        {
            throw new NotImplementedException();
        }

        public int SetPrice(int idDish, int price)
        {
            throw new NotImplementedException();
        }

        public List<Dish> GetAllDishesByRestaurant(int idRestaurant)
        {
            throw new NotImplementedException();
        }

        public Dish GetDishById(int idDish)
        {
            throw new NotImplementedException();
        }
    }
}
