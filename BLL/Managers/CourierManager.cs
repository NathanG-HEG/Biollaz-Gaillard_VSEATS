using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.BusinessExceptions;
using BLL.Interfaces;
using DataAccessLayer;
using DataAccessLayer.DBAccesses;

namespace BLL
{
    public class CourierManager:IntICourierManager

    {
    private CouriersDB CouriersDb { get; }
    private DeliveryAreasDB DeliveryAreasDb { get; }
    private OrdersDB OrdersDb { get; }

    public CourierManager()
    {
        CouriersDb = new CouriersDB();
        DeliveryAreasDb = new DeliveryAreasDB();
        OrdersDb = new OrdersDB();
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
            Courier res = CouriersDb.GetCourierByLogin(emailAddress, password);
            if (res == null) throw new DataBaseException("Email or password incorrect");
            return res;
        }

        public Courier GetCourierById(int idCourier)
        {
            return CouriersDb.GetCourierById(idCourier);
        }
    }
}
