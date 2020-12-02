using System;
using System.ComponentModel.DataAnnotations;

namespace RijlesPlanner.UI.MVC.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First name")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Za-z]+[\s][A-Za-z]+[.][A-Za-z]+$", ErrorMessage = "Please enter a valid first name.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Za-z]+[\s][A-Za-z]+[.][A-Za-z]+$", ErrorMessage = "Please enter a valid last name.")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
