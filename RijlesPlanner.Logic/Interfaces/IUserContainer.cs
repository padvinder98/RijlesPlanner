using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RijlesPlanner.ApplicationCore.Models;
using RijlesPlanner.ApplicationCore.Results;

namespace RijlesPlanner.ApplicationCore.Interfaces
{
    public interface IUserContainer
    {
        public Task<User> GetUserByEmailAddressAsync(string emailAddress);
        public Task<User> GetUserByIdAsync(Guid id);
        public Task<bool> DoesEmailAddressExistsAsync(string emailAddress);
        public Task<UserResult> CreateNewUserAsync(User user, string password);
        public Task<bool> DoesPasswordsMatchAsync(string emailAddress, string password);
        public Task UpdateUserAsync(User user);
        public Task<List<User>> GetAllStudents(Guid roleId);
        public Task<int> AddStudentToInstructorAsync(Guid instructorId, Guid studentId);
        public Task<int> RemoveStudentFromInstructorAsync(Guid instructorId, Guid studentId);
        public Task<List<User>> GetAllStudentsByInstructorAsync(Guid instructorId);
    }
}
