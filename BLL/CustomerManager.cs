using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DBAccesses;

namespace BLL
{
    public class CustomerManager
    {
        private CustomersDB CustomersDb { get; }
        private OrdersDB OrdersDb { get; }

        public CustomerManager()
        {
            CustomersDb = new CustomersDB();
            OrdersDb = new OrdersDB();
        }
    }
}
