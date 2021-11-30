using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DataAccessLayer;
using DataAccessLayer.DBAccesses;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class DeliveryAreaManager:IDeliveryAreaManager
    {
        private IConfiguration Configuration { get; }
        public DeliveryAreaManager(IConfiguration configuration)
        {
            Configuration = configuration;
            DeliveryAreasDb = new DeliveryAreasDB(Configuration);
        }

        private DeliveryAreasDB DeliveryAreasDb { get; }

        public DeliveryArea GetDeliveryAreaByName(string name)
        {
            return DeliveryAreasDb.GetDeliveryAreaByName(name);
        }

        public DeliveryArea GetDeliveryAreaByPostcode(int postcode)
        {
            return DeliveryAreasDb.GetDeliveryAreaByPostcode(postcode);
        }

        public List<DeliveryArea> GetAllDeliveryAreas()
        {
            return DeliveryAreasDb.GetAllDeliveryAreas();
        }
    }
}
