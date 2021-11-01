using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DataAccessLayer;
using DataAccessLayer.DBAccesses;

namespace BLL
{
    public class DeliveryAreaManager:IDeliveryAreaManager
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

        public DeliveryArea GetDeliveryAreaByName(string name)
        {
            return DeliveryAreasDb.GetDeliveryAreaByName(name);
        }

        public DeliveryArea GetDeliveryAreaByPostcode(int postcode)
        {
            return DeliveryAreasDb.GetDeliveryAreaByPostcode(postcode);
        }
    }
}
