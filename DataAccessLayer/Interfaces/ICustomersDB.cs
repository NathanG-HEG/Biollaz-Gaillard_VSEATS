using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ICustomersDB
    {
        public int AddCustomer(string firstname, string lastname, string emailAddress, string password);
        public Customer GetCustomerByLogin(string emailAddress, string password);
        public Customer GetCustomerById(int idCustomer);
    }
}
