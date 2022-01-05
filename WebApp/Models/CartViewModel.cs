using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace WebApp.Models
{
    /// <summary>
    /// Class to model a cart. It comports a list of composition object. Composition objects are compound by a reference to a dish, its quantity and
    /// a reference to its order.
    /// </summary>
    public class CartViewModel
    {
       public List<CompositionViewModel> Compositions { get; set; }

    }
}
