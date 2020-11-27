using System;
using System.ComponentModel.DataAnnotations;

namespace RijlesPlanner.ApplicationCore.ViewModels.AccountViewModels
{
    public class ProfileViewModel
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Street name")]
        public string StreetName { get; set; }
        [Display(Name = "House number")]
        public string HouseNumber { get; set; }
    }
}
