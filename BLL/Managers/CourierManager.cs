using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.BusinessExceptions;
using BLL.Interfaces;
using DataAccessLayer;
using DataAccessLayer.DBAccesses;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class CourierManager:ICourierManager

    {
    private CouriersDB CouriersDb { get; }
    private Utilities Utilities { get; }
    private IConfiguration Configuration { get; }

    public CourierManager(IConfiguration configuration)
    {
        Configuration = configuration;
        CouriersDb = new CouriersDB(Configuration);
    }

        public void AddCourier(int idArea, string firstName, string lastName, string emailAddress, string password)
        {
            //Checks if email and passwords syntax are correct
            if (!Utilities.IsEmailAddressCorrect(emailAddress))
                throw new InputSyntaxException(emailAddress + " is not valid");
            if(!Utilities.IsPasswordSyntaxCorrect(password))
                throw new InputSyntaxException("Password must contain at least 8 characters, a number and a capital");
            // Checks if email address is redundant
            if (Utilities.IsEmailAddressInDatabase(emailAddress))
                throw new BusinessRuleException("An account using this email address already exists");


            if (CouriersDb.AddCourier(idArea, firstName, lastName, emailAddress, password) == 0)
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
            return CouriersDb.GetCourierByLogin(emailAddress, password);
        }

        public Courier GetCourierById(int idCourier)
        {
            return CouriersDb.GetCourierById(idCourier);
        }
    }
}
