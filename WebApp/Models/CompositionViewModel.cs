﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models
{
    public class CompositionViewModel
    {
        public int idDish { get; set; }
        public int quantity { get; set; }

       
    }
}
