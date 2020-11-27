using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RijlesPlanner.ApplicationCore.Models;
using RijlesPlanner.ApplicationCore.Results;
using RijlesPlanner.ApplicationCore.ViewModels.AccountViewModels;

namespace RijlesPlanner.ApplicationCore.Interfaces
{
    public interface IUserContainer
    {
        public Task<User> GetUserByEmailAddressAsync(string emailAddress);
        public Task<User> GetUserByIdAsync(Guid id);
        public Task<bool> DoesEmailAddressExistsAsync(string emailAddress);
        public Task<UserResult> CreateNewUserAsync(User user, string password);
        public Task<bool> DoesPasswordsMatchAsync(string emailAddress, string password);
        public Task<ProfileViewModel> GetProfileByEmailAddressAsync(string emailAddress);
        public Task UpdateUserAsync(User user);
        public Task<List<User>> GetAllStudents(Guid roleId);
    }
}
