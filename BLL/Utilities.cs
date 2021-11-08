using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataAccessLayer.DBAccesses;

namespace BLL
{
    public abstract class Utilities
    {
        public static readonly int MaxQuantity = 999_999_999;
        public static readonly int MaxOrdersSimultaneously = 2;

        public static bool IsEmailAddressInDatabase(string emailAddress)
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
            CouriersDB couriersDb = new CouriersDB();
            List<Courier> couriers = couriersDb.GetAllCouriers();
            foreach (var c in couriers)
            {
                if (c.EmailAddress.Equals(emailAddress))
                {
                    return true;
                }
            }
            RestaurantsDB restaurantsDb = new RestaurantsDB();
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


        public static bool IsEmailAddressCorrect(string emailAddress)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex rg = new Regex(pattern);

            return rg.IsMatch(emailAddress);
        }

        public static bool IsPasswordSyntaxCorrect(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            return (hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) &&
                    hasMinimum8Chars.IsMatch(password));
        }

    }
}
