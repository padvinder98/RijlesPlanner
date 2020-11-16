using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RijlesPlanner.IDataAccessLayer.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string EmailAddress { get; set; }

        public UserDto(Guid id, string firstName, string lastName, string emailAddress, DateTime birthDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            EmailAddress = emailAddress;
        }

        public UserDto(string firstName, string lastName, DateTime birthDate, string emailAddress)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            EmailAddress = emailAddress;
        }
    }
}
