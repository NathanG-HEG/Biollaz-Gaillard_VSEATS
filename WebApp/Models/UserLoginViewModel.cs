using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class UserLoginViewModel
    {
        public string emailAddress { get; set; }
        public string password { get; set; }

    }
}
