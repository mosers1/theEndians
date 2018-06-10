using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _5051.Models
{
    /// <summary>
    /// View Model for the credentials used on login screen.
    /// </summary>
    public class CredentialsViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

 
}