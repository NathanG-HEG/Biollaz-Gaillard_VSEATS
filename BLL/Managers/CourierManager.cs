using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BLL.BusinessExceptions;
using BLL.Interfaces;
using DataAccessLayer;
using DataAccessLayer.DBAccesses;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BLL
{
    public class CourierManager : ICourierManager

    {
        private CouriersDB CouriersDb { get; }
        private Utilities Utilities { get; }
        private IConfiguration Configuration { get; }

        public CourierManager(IConfiguration configuration)
        {
            Configuration = configuration;
            CouriersDb = new CouriersDB(Configuration);
            Utilities = new Utilities(Configuration);
        }

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

            if (CouriersDb.AddCourier(idArea, firstName, lastName, emailAddress, pwdHash, salt) == 0)
            {
                // AddCourier == 0 means no row were affected
                throw new DataBaseException("Courier could not be added");
            }
        }

        public List<Courier> GetAllCouriersByArea(int idArea)
        {
            var res = CouriersDb.GetAllCouriersByArea(idArea);
            if (res == null)
                throw new DataBaseException("No couriers could be found in area " + idArea +
                                            " or the area does not exist");
            return res;
        }

        public Courier GetCourierByLogin(string emailAddress, string password)
        {
            List<Courier> couriers = CouriersDb.GetAllCouriers();

            foreach (var c in couriers)
            {
                if (!emailAddress.Equals(c.EmailAddress)) continue;

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

        public Courier GetCourierById(int idCourier)
        {
            return CouriersDb.GetCourierById(idCourier);
        }
    }
}
