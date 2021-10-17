using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IDeliveryAreasDB
    {
        public void AddDeliveryArea(string name, int postcode);
        public DeliveryArea GetDeliveryAreaByName(string name);
        public DeliveryArea GetDeliveryAreaByPostcode(int postcode);

    }
}
