using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.BusinessExceptions;
using BLL.Interfaces;
using DataAccessLayer.DBAccesses;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class CustomerManager:ICustomerManager
    {
        private CustomersDB CustomersDb { get; }
        private IConfiguration Configuration { get; }
        private Utilities Utilities { get; }

        public CustomerManager(IConfiguration configuration)
        {
            Configuration = configuration;
            Utilities = new Utilities(Configuration);
            CustomersDb = new CustomersDB();
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
            return CustomersDb.GetCustomerByLogin(emailAddress, password);
        }

        public Customer GetCustomerById(int idCustomer)
        {
            return CustomersDb.GetCustomerById(idCustomer);
        }
    }
}
