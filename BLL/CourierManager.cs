using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.DBAccesses;

namespace BLL
{
    public class CourierManager
    {
        private CouriersDB CouriersDb { get; }
        private DeliveryAreasDB DeliveryAreasDb { get; }
        private OrdersDB OrdersDb { get; }

        public CourierManager()
        {
            CouriersDb = new CouriersDB();
            DeliveryAreasDb = new DeliveryAreasDB();
            OrdersDb = new OrdersDB();
        }
    }
}
