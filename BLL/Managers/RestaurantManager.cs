using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using BLL.BusinessExceptions;
using BLL.Interfaces;
using DataAccessLayer.DBAccesses;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    /// <summary>
    /// Manager to create, change and get restaurants
    /// </summary>
    public class RestaurantManager : IRestaurantManager

    {
        private RestaurantsDB RestaurantsDb { get; }
        private IConfiguration Configuration { get; }
        private Utilities Utilities { get; }

        /// <summary>
        /// Manager constructor
        /// </summary>
        /// <param name="configuration">The configuration used to inject the manager</param>
        public RestaurantManager(IConfiguration configuration)
        {
            Configuration = configuration;
            Utilities = new Utilities(Configuration);
            RestaurantsDb = new RestaurantsDB(Configuration);
        }

        /// <summary>
        /// Method to create a restaurant
        /// </summary>
        /// <param name="idArea">The primary key of the area in which the restaurant is located</param>
        /// <param name="name">The name of the restaurant</param>
        /// <param name="emailAddress">The email address of the restaurant</param>
        /// <param name="password">The password of the restaurant</param>
        public void CreateRestaurant(int idArea, string name, string emailAddress, string password)
        {

            // Checks for syntax errors
            if (!Utilities.IsEmailAddressCorrect(emailAddress))
                throw new InputSyntaxException(emailAddress + " is not valid");
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
            
            int result = RestaurantsDb.AddRestaurant(idArea, name, emailAddress, pwdHash, salt);
            if (result == 0)
            {
                throw new DataBaseException("Error occurred, restaurant " + name + " was not created.");
            }
        }

        /// <summary>
        /// Method to get a restaurant using its login
        /// </summary>
        /// <param name="email">The email address used to log in</param>
        /// <param name="password">The password used to log in</param>
        /// <returns>Returns a Restaurant object. If login failed, returns null</returns>
        public Restaurant GetRestaurantByLogin(string email, string password)
        {
            List<Restaurant> restaurants = RestaurantsDb.GetAllRestaurants();

            foreach (var r in restaurants)
            {
                if (!email.Equals(r.EmailAddress)) continue;

                byte[] saltBytes = Convert.FromBase64String(r.Salt);
                string pwdHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: saltBytes,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
                Restaurant restaurant = RestaurantsDb.GetRestaurantByLogin(r.EmailAddress, pwdHash);
                if (restaurant != null)
                {
                    return restaurant;
                }
            }

            return null;
        }

        /// <summary>
        /// Method to get all restaurants stored in the database
        /// </summary>
        /// <returns>Returns a list of Restaurant object</returns>
        public List<Restaurant> GetAllRestaurants()
        {
            return RestaurantsDb.GetAllRestaurants();
        }

        /// <summary>
        /// Method to get a restaurant using its primary key
        /// </summary>
        /// <param name="id">The primary key used in the database</param>
        /// <returns>Returns a Restaurant object. Returns null if no Restaurant were found</returns>
        public Restaurant GetRestaurantById(int id)
        {
            return RestaurantsDb.GetRestaurantById(id);
        }

        /// <summary>
        /// Method to get a restaurant from which an order has been placed
        /// </summary>
        /// <param name="idOrder">The primary key of the order</param>
        /// <returns>Returns a Restaurant object. Returns null if not found.</returns>
        public Restaurant GetRestaurantByOrder(int idOrder)
        {
            return RestaurantsDb.GetRestaurantByOrder(idOrder);
        }
    }
}
