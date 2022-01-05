using System.Collections.Generic;
using DTO;

namespace DataAccessLayer.Interfaces
{
    public interface ICouriersDB
    {
        public int AddCourier(int idArea, string firstName, string lastName, string emailAddress, string pwdHash, string salt);
        public List<Courier> GetAllCouriersByArea(int idArea);
        public Courier GetCourierByLogin(string emailAddress, string password);
        public Courier GetCourierById(int idCourier);
        public List<Courier> GetAllCouriers();
    }
}
