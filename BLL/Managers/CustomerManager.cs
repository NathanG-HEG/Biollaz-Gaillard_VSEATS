using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DataAccessLayer.DBAccesses;

namespace BLL
{
    public class CustomerManager:ICustomerManager
    {
        private CustomersDB CustomersDb { get; }
        private OrdersDB OrdersDb { get; }

        public CustomerManager()
        {
            CustomersDb = new CustomersDB();
            OrdersDb = new OrdersDB();
        }

        public int CreateCustomer(string firstname, string lastname, string emailAddress, string password)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByLogin(string emailAddress, string password)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerById(int idCustomer)
        {
            throw new NotImplementedException();
        }
    }
}
