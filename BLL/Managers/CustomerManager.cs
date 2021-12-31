using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BLL.BusinessExceptions;
using BLL.Interfaces;
using DataAccessLayer.DBAccesses;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
            CustomersDb = new CustomersDB(Configuration);
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

            var saltBytes = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(saltBytes);
            }

            string pwdHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            string salt = Convert.ToBase64String(saltBytes);

            if (CustomersDb.AddCustomer(firstname, lastname, emailAddress, pwdHash, salt) == 0)
            {
                // AddCustomer == 0 means no row were affected
                throw new DataBaseException("Customer could not be added");
            }
                
        }

        public Customer GetCustomerByLogin(string emailAddress, string password)
        {
            List<Customer> customers = CustomersDb.GetAllCustomers();

            foreach (var c in customers)
            {
                if (!emailAddress.Equals(c.EmailAddress)) continue;

                byte[] saltBytes = Convert.FromBase64String(c.Salt);
                string pwdHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: saltBytes,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
                Customer customer = CustomersDb.GetCustomerByLogin(c.EmailAddress, pwdHash);
                if (customer != null)
                {
                    return customer;
                }
            }
            return null;
        }

        public Customer GetCustomerById(int idCustomer)
        {
            return CustomersDb.GetCustomerById(idCustomer);
        }
    }
}
