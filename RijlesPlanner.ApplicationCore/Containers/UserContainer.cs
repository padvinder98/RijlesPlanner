using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using RijlesPlanner.ApplicationCore.Interfaces;
using RijlesPlanner.ApplicationCore.Models;
using RijlesPlanner.ApplicationCore.Results;
using RijlesPlanner.IData.Dtos;
using RijlesPlanner.IData.Interfaces;

namespace RijlesPlanner.ApplicationCore.Containers
{
    public class UserContainer : IUserContainer
    {
        private readonly IUserRepository _userRepository;

        public UserContainer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> AddStudentToInstructorAsync(Guid instructorId, Guid studentId)
        {
            return await _userRepository.AddStudentToInstructorAsync(instructorId, studentId);
        }

        public async Task<UserResult> CreateNewUserAsync(User user, string password)
        {
            UserResult userResult = new UserResult();

            if (await _userRepository.DoesEmailAddressExistsAsync(user.EmailAddress))
            {
                userResult.AddUserResultError(new Error("This user already exists."));
                userResult.SetFailed();
            }
            else
            {
                var salt = CreateSalt();
                var hashedPassword = HashPassword(password, salt);

                if (await _userRepository.CreateNewUserAsync(new UserDto(user.FirstName, user.LastName, user.DateOfBirth, user.EmailAddress, new RoleDto(user.Role.Id, user.Role.Name)), hashedPassword, salt) != 1)
                {
                    userResult.AddUserResultError(new Error("Something went wrong"));
                    userResult.SetFailed();
                }
            }

            return userResult;
        }

        public async Task<bool> DoesEmailAddressExistsAsync(string emailAddress)
        {
            return await _userRepository.DoesEmailAddressExistsAsync(emailAddress);
        }

        public async Task<bool> DoesPasswordsMatchAsync(string emailAddress, string password)
        {
            var salt = await _userRepository.GetSaltByEmailaddressAsync(emailAddress);

            return await _userRepository.DoesPasswordsMatchAsync(emailAddress, HashPassword(password, salt));
        }

        public async Task<List<User>> GetAllStudents(Guid roleId)
        {
            var result = await _userRepository.GetAllStudentsAsync(roleId);

            return result.Select(u => new User(u)).ToList();
        }

        public async Task<List<User>> GetAllStudentsByInstructorAsync(Guid instructorId)
        {
            var result = await _userRepository.GetAllStudentsByInstructorAsync(instructorId);

            return result.Select(u => new User(u)).ToList();
        }

        public async Task<User> GetUserByEmailAddressAsync(string emailAddress)
        {
            return new User(await _userRepository.GetUserByEmailAddressAsync(emailAddress));
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return new User(await _userRepository.GetUserByIdAsync(id));
        }

        public async Task<int> RemoveStudentFromInstructorAsync(Guid instructorId, Guid studentId)
        {
            return await _userRepository.RemoveStudentFromInstructorAsync(instructorId, studentId);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateUserAsync(new UserDto { Id= user.Id, FirstName = user.FirstName, LastName = user.LastName, DateOfBirth = user.DateOfBirth, City = user.City, StreetName = user.StreetName, HouseNumber = user.HouseNumber });
        }

        private string CreateSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        private string HashPassword(string password, string salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
