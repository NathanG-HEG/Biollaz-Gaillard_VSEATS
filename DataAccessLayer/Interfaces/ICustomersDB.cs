using System.Collections.Generic;
using DTO;

namespace DataAccessLayer.Interfaces
{
    public interface ICustomersDB
    {
        public int AddCustomer(string firstname, string lastname, string emailAddress, string pwdHash, string salt);
        public Customer GetCustomerByLogin(string emailAddress, string pwdHash);
        public Customer GetCustomerById(int idCustomer);
        public List<Customer> GetAllCustomers();
    }
}
