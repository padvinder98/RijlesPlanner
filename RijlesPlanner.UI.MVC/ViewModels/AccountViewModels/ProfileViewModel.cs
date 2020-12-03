using System;
using System.ComponentModel.DataAnnotations;

namespace RijlesPlanner.UI.MVC.ViewModels.AccountViewModels
{
    public class ProfileViewModel
    {
        [Required]
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Achternaam")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Geboortedatum")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "E-mailadres")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Display(Name = "Stad")]
        public string City { get; set; }
        [Display(Name = "Straat")]
        public string StreetName { get; set; }
        [Display(Name = "Huisnummer")]
        public string HouseNumber { get; set; }
    }
}
