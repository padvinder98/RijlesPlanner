using System;
using RijlesPlanner.IData.Dtos;

namespace RijlesPlanner.ApplicationCore.Models
{
    public class User
    {
        public Guid Id { get; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string EmailAddress { get; }
        public string City { get; private set; }
        public string StreetName { get; private set; }
        public string HouseNumber { get; private set; }
        public Role Role { get; }

        public User(UserDto userDto)
        {
            this.Id = userDto.Id;
            this.FirstName = userDto.FirstName;
            this.LastName = userDto.LastName;
            this.DateOfBirth = userDto.DateOfBirth;
            this.EmailAddress = userDto.EmailAddress;
            this.City = userDto.City;
            this.StreetName = userDto.StreetName;
            this.HouseNumber = userDto.HouseNumber;
            this.Role = new Role(userDto.Role);
        }

        public User(string firstName, string lastName, DateTime dateOfBirth, string emailAddress, Role role)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.EmailAddress = emailAddress;
            this.Role = role;
        }

        public void Update(string firstName, string lastName, DateTime dateOfBirth, string city, string streetName, string houseNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            City = city;
            StreetName = streetName;
            HouseNumber = houseNumber;
        }
    }
}
