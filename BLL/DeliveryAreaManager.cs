using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.DBAccesses;

namespace BLL
{
    public class DeliveryAreaManager
    {
        private DeliveryAreasDB DeliveryAreasDb { get; }
        private CouriersDB CouriersDb { get; }
        private OrdersDB OrderDb { get; }
        private RestaurantsDB RestaurantDb { get; }

        public DeliveryAreaManager()
        {
            DeliveryAreasDb = new DeliveryAreasDB();
            CouriersDb = new CouriersDB();
            OrderDb = new OrdersDB();
            RestaurantDb = new RestaurantsDB();
        }
    }
}
