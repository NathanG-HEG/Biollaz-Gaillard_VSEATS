using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DBAccesses;

namespace BLL
{
    public class DishManager
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
    }
}
