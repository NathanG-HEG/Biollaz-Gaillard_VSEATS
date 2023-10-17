using System;

namespace DTO
{

/// <summary>
/// Class to model an order
/// </summary>
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

       
    }

}
