using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataAccessLayer.DBAccesses;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class Utilities
    {
        public  readonly int MaxQuantity = 999_999_999;
        public  readonly int MaxOrdersSimultaneously = 2;

        public IConfiguration Configuration;
        public Utilities(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public  bool IsEmailAddressInDatabase(string emailAddress)
        {
            CustomersDB customersDb = new CustomersDB();
            List<Customer> customers = customersDb.GetAllCustomers();
            foreach (var c in customers)
            {

                if (c.EmailAddress.Equals(emailAddress))
                {
                    return true;
                }
            }
            CouriersDB couriersDb = new CouriersDB(Configuration);
            List<Courier> couriers = couriersDb.GetAllCouriers();
            foreach (var c in couriers)
            {
                if (c.EmailAddress.Equals(emailAddress))
                {
                    return true;
                }
            }
            
            RestaurantsDB restaurantsDb = new RestaurantsDB(Configuration);
            List<Restaurant> restaurants= restaurantsDb.GetAllRestaurants();
            foreach (var r in restaurants)
            {
                if (r.EmailAddress.Equals(emailAddress))
                {
                    return true;
                }
            }
            

            return false;
        }


        public bool IsEmailAddressCorrect(string emailAddress)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex rg = new Regex(pattern);

            return rg.IsMatch(emailAddress);
        }

        public bool IsPasswordSyntaxCorrect(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            return (hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) &&
                    hasMinimum8Chars.IsMatch(password));
        }

    }
}
