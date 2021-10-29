using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DBAccesses;

namespace BLL
{
    public class RestaurantManager
    {
        private RestaurantsDB RestaurantsDb { get; }
        private DishesDB DishesDb { get; }
        private DeliveryAreasDB DeliveryAreasDb {get}

        public RestaurantManager()
        {
            RestaurantsDb = new RestaurantsDB();
            DishesDb = new DishesDB();
            DeliveryAreasDb = new DeliveryAreasDB();
        }
    }
}
