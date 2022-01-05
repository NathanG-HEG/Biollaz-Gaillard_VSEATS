using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{

    /// <summary>
    /// Generic login class which stores a user's email address and password. It is being used for customers, couriers and restaurants.
    /// </summary>
    public class UserLoginViewModel
    {
        [EmailAddress, Required]
        public string emailAddress { get; set; }
        [Required]
        public string password { get; set; }

    }
}
