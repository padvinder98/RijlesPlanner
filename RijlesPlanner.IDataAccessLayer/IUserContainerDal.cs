using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RijlesPlanner.IDataAccessLayer.Dtos;

namespace RijlesPlanner.IDataAccessLayer
{
    public interface IUserContainerDal
    {
        public Task<UserDto> GetUserByEmailAddressAsync(string emailAddress);
        public Task<UserDto> GetUserByIdAsync(Guid id);
        public Task<bool> DoesEmailAddressExistsAsync(string emailAddress);
        public Task<int> CreateNewUserAsync(UserDto userDto, string password, string salt);
        public Task<string> GetSaltByEmailaddressAsync(string emailAddress);
        public Task<bool> DoesPasswordsMatchAsync(string emailAddress, string password);
        public Task<int> UpdateUserAsync(UserDto userDto);
        public Task<List<UserDto>> GetAllStudents(Guid roleId);
    }
}
