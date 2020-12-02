using System;
namespace RijlesPlanner.IData.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public RoleDto Role { get; set; }

        public UserDto()
        {

        }

        public UserDto(string firstName, string lastName, DateTime dateOfBirth, string emailAddress, RoleDto roleDto)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            EmailAddress = emailAddress;
            Role = roleDto;
        }
    }
}
