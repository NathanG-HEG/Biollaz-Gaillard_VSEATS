using System.Collections.Generic;
using BLL.Interfaces;
using DataAccessLayer.DBAccesses;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    /// <summary>
    /// Manager to get delivery areas
    /// </summary>
    public class DeliveryAreaManager:IDeliveryAreaManager
    {
        private IConfiguration Configuration { get; }
        /// <summary>
        /// Manager constructor
        /// </summary>
        /// <param name="configuration">The configuration used to inject the manager</param>
        public DeliveryAreaManager(IConfiguration configuration)
        {
            Configuration = configuration;
            DeliveryAreasDb = new DeliveryAreasDB(Configuration);
        }

        private DeliveryAreasDB DeliveryAreasDb { get; }

        /// <summary>
        /// Method to get a delivery area by its name
        /// </summary>
        /// <param name="name">The name of the area looked for</param>
        /// <returns>Returns a DeliveryArea object. Returns null if no such area exists</returns>
        public DeliveryArea GetDeliveryAreaByName(string name)
        {
            return DeliveryAreasDb.GetDeliveryAreaByName(name);
        }

        /// <summary>
        /// Method to get a delivery area by its postcode
        /// </summary>
        /// <param name="postcode">The post code of the area looked for</param>
        /// <returns>Returns a DeliveryArea object. Returns null it it doesn't exist</returns>
        public DeliveryArea GetDeliveryAreaByPostcode(int postcode)
        {
            return DeliveryAreasDb.GetDeliveryAreaByPostcode(postcode);
        }

        /// <summary>
        /// Method to get all delivery areas
        /// </summary>
        /// <returns>Returns a list of every DeliveryArea objects stored in the database</returns>
        public List<DeliveryArea> GetAllDeliveryAreas()
        {
            return DeliveryAreasDb.GetAllDeliveryAreas();
        }

        /// <summary>
        /// Method to get a delivery area using its id in the database
        /// </summary>
        /// <param name="id">The primary key of the delivery area in the database</param>
        /// <returns>Returns a delivery area object</returns>
        public DeliveryArea GetDeliveryAreaById(int id)
        {
            return DeliveryAreasDb.GetDeliveryAreaById(id);
        }
    }
}
