using System.Collections.Generic;
using DTO;

namespace DataAccessLayer.Interfaces
{
    public interface IDeliveryAreasDB
    {
        public int AddDeliveryArea(string name, int postcode);
        public DeliveryArea GetDeliveryAreaByName(string name);
        public DeliveryArea GetDeliveryAreaByPostcode(int postcode);
        public List<DeliveryArea> GetAllDeliveryAreas();
        public DeliveryArea GetDeliveryAreaById(int id);
    }
}
