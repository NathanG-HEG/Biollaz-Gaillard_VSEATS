using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models
{
    public class CompositionViewModel
    {
        public int IdDish { get; set; }
        public int Quantity { get; set; }
        public string DishImagePath { get; set; }
        public double DishPrice { get; set; }
        public string DishName { get; set; }
       
    }
}
