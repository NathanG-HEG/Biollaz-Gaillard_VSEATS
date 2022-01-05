using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using BLL.BusinessExceptions;
using BLL.Interfaces;
using DataAccessLayer.DBAccesses;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    /// <summary>
    /// The manager used to create and get Customers
    /// </summary>
    public class CustomerManager:ICustomerManager
    {
        private CustomersDB CustomersDb { get; }
        private IConfiguration Configuration { get; }
        private Utilities Utilities { get; }

        /// <summary>
        /// The manager constructor
        /// </summary>
        /// <param name="configuration">The configuration used to inject the manager</param>
        public CustomerManager(IConfiguration configuration)
        {
            Configuration = configuration;
            Utilities = new Utilities(Configuration);
            CustomersDb = new CustomersDB(Configuration);
        }

        /// <summary>
        /// Method to create a customer
        /// </summary>
        /// <param name="firstname">The customer first name</param>
        /// <param name="lastname">The customer last name</param>
        /// <param name="emailAddress">The customer email address</param>
        /// <param name="password">The customer password</param>
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

            //Creates a 16bytes random salt
            var saltBytes = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(saltBytes);
            }
            //Creates a password hash using the given password and the hash
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

        /// <summary>
        /// Method to get a customer using his login
        /// </summary>
        /// <param name="emailAddress">The email address used to log in</param>
        /// <param name="password">The password used to log in</param>
        /// <returns>Returns a Customer object if the login is correct. Returns null if incorrect</returns>
        public Customer GetCustomerByLogin(string emailAddress, string password)
        {
            List<Customer> customers = CustomersDb.GetAllCustomers();

            foreach (var c in customers)
            {
                if (!emailAddress.Equals(c.EmailAddress)) continue;
                //Try to match the given password + salt hashed to the hash stored in the database
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

        /// <summary>
        /// Method to get a customer using his database id
        /// </summary>
        /// <param name="idCustomer">The id used to get the customer</param>
        /// <returns>Returns a customer if it exists. Return null if no customer with this id exists.</returns>
        public Customer GetCustomerById(int idCustomer)
        {
            return CustomersDb.GetCustomerById(idCustomer);
        }
    }
}
