using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    /// <summary>
    /// Class to model a dish. It is a subset of the DTO's dish object. 
    /// </summary>
    public class DishViewModel
    {
        public int IdDish { get; set; }
        public int IdRestaurant { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    }
}
