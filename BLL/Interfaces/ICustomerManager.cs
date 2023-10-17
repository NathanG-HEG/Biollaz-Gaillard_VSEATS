
using DTO;

namespace BLL.Interfaces
{
    public interface ICustomerManager
    {
        public void CreateCustomer(string firstname, string lastname, string emailAddress, string password);
        public Customer GetCustomerByLogin(string emailAddress, string password);
        public Customer GetCustomerById(int idCustomer);

    }
}
