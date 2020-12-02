using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RijlesPlanner.IData.Dtos;

namespace RijlesPlanner.IData.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserDto> GetUserByEmailAddressAsync(string emailAddress);
        public Task<UserDto> GetUserByIdAsync(Guid id);
        public Task<bool> DoesEmailAddressExistsAsync(string emailAddress);
        public Task<int> CreateNewUserAsync(UserDto userDto, string password, string salt);
        public Task<string> GetSaltByEmailaddressAsync(string emailAddress);
        public Task<bool> DoesPasswordsMatchAsync(string emailAddress, string password);
        public Task<int> UpdateUserAsync(UserDto userDto);
        public Task<IEnumerable<UserDto>> GetAllStudentsAsync(Guid roleId);
        public Task<IEnumerable<UserDto>> GetAllStudentsByInstructorAsync(Guid instructorId);
        public Task<int> AddStudentToInstructorAsync(Guid instructorId, Guid studentId);
        public Task<int> RemoveStudentFromInstructorAsync(Guid instructorId, Guid studentId);
    }
}
