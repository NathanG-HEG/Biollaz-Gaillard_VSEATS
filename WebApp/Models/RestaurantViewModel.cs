using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class RestaurantViewModel
    {
        public string Name { get; set; }
        public string AreaName { get; set; }
        public string ImagePath { get; set; }
        public string IconPath { get; set; }
        public int IdRestaurant { get; set; }
        public List<DishViewModel> Dishes { get; set; }

    }
}
