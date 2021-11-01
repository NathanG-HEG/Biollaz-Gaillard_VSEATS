using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface ICustomerManager
    {
        public void CreateCustomer(string firstname, string lastname, string emailAddress, string password);
        public Customer GetCustomerByLogin(string emailAddress, string password);
        public Customer GetCustomerById(int idCustomer);

    }
}
