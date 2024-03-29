﻿using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    /// <summary>
    /// Used to display and pass information when a delivery man signs up. 
    /// </summary>
    public class CourierSignUpViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordConfirmation { get; set; }
        [Required]
        public int PostCode { get; set; }
    }
}
