using System;
using System.ComponentModel.DataAnnotations;

namespace RijlesPlanner.UI.MVC.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Voornaam")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Za-z]+[\s][A-Za-z]+[.][A-Za-z]+$", ErrorMessage = "Dit is geen geldige voornaam.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Achternaam")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Za-z]+[\s][A-Za-z]+[.][A-Za-z]+$", ErrorMessage = "Dit is geen geldige achternaam.")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Geboortedatum")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "E-mailadres")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Wachtwoord")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Bevestig wachtwoord")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
