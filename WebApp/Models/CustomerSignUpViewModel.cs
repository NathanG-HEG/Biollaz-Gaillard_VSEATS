using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    /// <summary>
    /// Used to display and pass information when customer signs up. 
    /// </summary>
    public class CustomerSignUpViewModel
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
    }
}
