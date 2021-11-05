using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.BusinessExceptions;
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

        public void CreateCustomer(string firstname, string lastname, string emailAddress, string password)
        {
            // Checks for syntax errors
            if (!Utilities.IsEmailAddressCorrect(emailAddress))
                throw new InputSyntaxException(emailAddress+" is not valid");
            if (!Utilities.IsPasswordSyntaxCorrect(password))
                throw new InputSyntaxException("Password must contain at least 8 characters, a number and a capital");

            //check if email address is redundant
            if (Utilities.IsEmailAddressInDatabase(emailAddress))
                throw new BusinessRuleException("An account using this email address already exists");

            if (CustomersDb.AddCustomer(firstname, lastname, emailAddress, password) == 0)
            {
                // AddCustomer == 0 means no row were affected
                throw new DataBaseException("Customer could not be added");
            }
                
        }

        public Customer GetCustomerByLogin(string emailAddress, string password)
        {
            Customer res = CustomersDb.GetCustomerByLogin(emailAddress, password);
            if (res == null) throw new DataBaseException("Email or password incorrect");
            return res;
        }

        public Customer GetCustomerById(int idCustomer)
        {
            return CustomersDb.GetCustomerById(idCustomer);
        }
    }
}
