using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.DBAccesses;

namespace BLL
{
    public class OrderManager
    {
        private OrdersDB OrdersDb { get; }
        private CustomersDB CustomersDb { get; }
        private CouriersDB CouriersDb { get; }
        private DeliveryAreasDB DeliveryAreasDb { get; }
        private CompositionDB CompositionDb { get; }

        public OrderManager()
        {
            OrdersDb = new OrdersDB();
            CustomersDb = new CustomersDB();
            CouriersDb = new CouriersDB();
            DeliveryAreasDb = new DeliveryAreasDB();
            CompositionDb = new CompositionDB();
        }
    }
}
