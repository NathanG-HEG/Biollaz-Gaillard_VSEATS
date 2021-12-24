using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;

namespace WebApp.Models
{
    public class OrderViewModel
    {
        public int IdOrder { get; set; }
        public DateTime ExpectedDeliveryTime { get; set; }
        public DateTime TimeOfDelivery { get; set; }
        public string DeliveryAddress { get; set; }
        public double OrderTotal { get; set; }
        public string AreaName { get; set; }
        public int PostCode { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string ImagePath { get; set; }
        public string RestaurantName { get; set; }
        public List<CompositionViewModel> OrderCompositions { get; set; }
        public List<CompositionViewModel> AvailableCompositions { get; set; }
        public List<DishViewModel> Dishes { get; set; } 

    }
}
