using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace WebApp.Models
{
    public class CartViewModel
    {
       public List<CompositionViewModel> Compositions { get; set; }

    }
}
