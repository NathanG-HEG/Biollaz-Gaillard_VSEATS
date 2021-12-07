using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{

    // File:    Order.cs
    // Author:  bbiol
    // Created: 17 October 2021 10:47:53
    // Purpose: Definition of Class Order

    public class Order
    {
        public int IdOrder { get; set; }
        public int IdCustomer { get; set; }
        public int IdCourier { get; set; }
        public DateTime ExpectedDeliveryTime { get; set; }
        public DateTime TimeOfOrder { get; set; }
        public DateTime TimeOfDelivery { get; set; }
        public string DeliveryAddress { get; set; }
        public int OrderTotal { get; set; }

        public int IdArea { get; set; }

        public override string ToString()
        {
            return "idOrder: " + IdOrder +
                   "\nidCustomer: " + IdCustomer +
                   "\nidCourier: " + IdCourier +
                   "\nexpectedDeliveryTime: " + ExpectedDeliveryTime +
                   "\ntimeOfOrder: " + TimeOfOrder +
                   "\ntimeOfDelivery: " + TimeOfDelivery +
                   "\ndeliveryAddress: " + DeliveryAddress +
                   "\norderTotal: " + OrderTotal +
                   "\nidArea: " + IdArea;
        }
    }

}
