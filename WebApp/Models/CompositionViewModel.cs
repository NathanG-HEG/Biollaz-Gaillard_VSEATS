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
        public int quantity { get; set; }
        public string dishImagePath { get; set; }
        public double dishPrice { get; set; }
        public string dishName { get; set; }
       
    }
}
