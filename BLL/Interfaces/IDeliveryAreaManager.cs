﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface IDeliveryAreaManager
    {
        public DeliveryArea GetDeliveryAreaByName(string name);
        public DeliveryArea GetDeliveryAreaByPostcode(int postcode);
    }
}