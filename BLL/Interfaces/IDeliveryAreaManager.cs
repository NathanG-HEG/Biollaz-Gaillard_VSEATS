﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
