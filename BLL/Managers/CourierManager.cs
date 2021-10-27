using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public int AddCourier(int idArea, string firstName, string lastName, string emailAddress, string password)
        {
            throw new NotImplementedException();
        }

        public List<Courier> GetAllCouriersByArea(int idArea)
        {
            throw new NotImplementedException();
        }

        public Courier GetCourierByLogin(string emailAddress, string password)
        {
            throw new NotImplementedException();
        }

        public Courier GetCourierById(int idCourier)
        {
            throw new NotImplementedException();
        }
    }
}
