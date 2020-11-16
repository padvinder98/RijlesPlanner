using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RijlesPlanner.ApplicationCore.Models.User
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string EmailAddress { get; set; }

        public User(string firstName, string lastName, DateTime birthDate, string emailAddress)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            EmailAddress = emailAddress;
        }
    }
}
