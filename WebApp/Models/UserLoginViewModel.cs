using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    
    public class UserLoginViewModel
    {
        [EmailAddress, Required]
        public string emailAddress { get; set; }
        [Required]
        public string password { get; set; }

    }
}
