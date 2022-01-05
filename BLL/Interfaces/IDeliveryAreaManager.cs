﻿
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IDeliveryAreaManager
    {
        public DeliveryArea GetDeliveryAreaByName(string name);
        public DeliveryArea GetDeliveryAreaByPostcode(int postcode);
        public List<DeliveryArea> GetAllDeliveryAreas();
        public DeliveryArea GetDeliveryAreaById(int id);
    }
}
