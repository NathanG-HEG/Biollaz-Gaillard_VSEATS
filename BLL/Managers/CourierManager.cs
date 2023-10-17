using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using BLL.BusinessExceptions;
using BLL.Interfaces;
using DataAccessLayer.DBAccesses;
using DTO;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;

namespace BLL.Managers
{
    /// <summary>
    /// The manager used to create and get courier from the database
    /// </summary>
    public class CourierManager : ICourierManager

    {
        private CouriersDB CouriersDb { get; }
        private Utilities Utilities { get; }
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Manager constructor
        /// </summary>
        /// <param name="configuration">The configuration used to inject the manager</param>
        public CourierManager(IConfiguration configuration)
        {
            Configuration = configuration;
            CouriersDb = new CouriersDB(Configuration);
            Utilities = new Utilities(Configuration);
        }
        /// <summary>
        /// Method to add a courier
        /// </summary>
        /// <param name="idArea">The id area in which the courier will work</param>
        /// <param name="firstName">The first name of the courier</param>
        /// <param name="lastName">The last name of the courier</param>
        /// <param name="emailAddress">The email address of the courier</param>
        /// <param name="password">The password of the courier</param>
        public void AddCourier(int idArea, string firstName, string lastName, string emailAddress, string password)
        {
            //Checks if email and passwords syntax are correct
            if (!Utilities.IsEmailAddressCorrect(emailAddress))
                throw new InputSyntaxException(emailAddress + " is not valid");
            if (!Utilities.IsPasswordSyntaxCorrect(password))
                throw new InputSyntaxException("Password must contain at least 8 characters, a number and a capital");
            // Checks if email address is redundant
            if (Utilities.IsEmailAddressInDatabase(emailAddress))
                throw new BusinessRuleException("An account using this email address already exists");

            // Creates a 16bytes random salt
            var saltBytes = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(saltBytes);
            }
            
            //Creates a password hash using the given password and the salt created
            string pwdHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            string salt = Convert.ToBase64String(saltBytes);

            if (CouriersDb.AddCourier(idArea, firstName, lastName, emailAddress, pwdHash, salt) == 0)
            {
                // AddCourier == 0 means no row were affected
                throw new DataBaseException("Courier could not be added");
            }
        }

        /// <summary>
        /// Method to get all courier in an area
        /// </summary>
        /// <param name="idArea">The id of the area in which the courier should be found</param>
        /// <returns>Returns a list of Courier</returns>
        public List<Courier> GetAllCouriersByArea(int idArea)
        {
            var res = CouriersDb.GetAllCouriersByArea(idArea);
            if (res == null)
                throw new DataBaseException("No couriers could be found in area " + idArea +
                                            " or the area does not exist");
            return res;
        }

        /// <summary>
        /// Method to get a courier using his login
        /// </summary>
        /// <param name="emailAddress">The email address used to log in</param>
        /// <param name="password">The password used to log in</param>
        /// <returns>Returns a Courier object if the login is correct. Returns null if incorrect</returns>
        public Courier GetCourierByLogin(string emailAddress, string password)
        {
            List<Courier> couriers = CouriersDb.GetAllCouriers();

            foreach (var c in couriers)
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
                Courier courier = CouriersDb.GetCourierByLogin(c.EmailAddress, pwdHash);
                if (courier != null)
                {
                    return courier;
                }
            }

            return null;
        }

        /// <summary>
        /// Method to get a courier using his database id
        /// </summary>
        /// <param name="idCourier">The id used to get the courier</param>
        /// <returns>Returns a courier if it exists. Return null if no courier with this id exists.</returns>
        public Courier GetCourierById(int idCourier)
        {
            return CouriersDb.GetCourierById(idCourier);
        }
    }
}
