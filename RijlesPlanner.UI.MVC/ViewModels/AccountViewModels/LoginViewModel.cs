using System;
using System.ComponentModel.DataAnnotations;

namespace RijlesPlanner.UI.MVC.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-mailadres")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Wachtwoord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Onthoud mij")]
        public bool RememberMe { get; set; }
    }
}
