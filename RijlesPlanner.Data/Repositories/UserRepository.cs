using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using RijlesPlanner.IData.Dtos;
using RijlesPlanner.IData.Interfaces;
using RijlesPlanner.IData.Interfaces.ConnectionFactory;

namespace RijlesPlanner.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnection _connectionFactory;

        public UserRepository(IConnection connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> AddStudentToInstructorAsync(Guid instructorId, Guid studentId)
        {
            var parameters = new { InstructorId = instructorId, StudentId = studentId };
            var sql = "INSERT INTO [dbo].[InstructorStudents]([InstructorId], [StudentId]) VALUES(@InstructorId, @StudentId)";

            return await SqlMapper.ExecuteAsync(_connectionFactory.GetConnection, sql, parameters);
        }

        public async Task<int> CreateNewUserAsync(UserDto userDto, string password, string salt)
        {
            var parameters = new { FirstName = userDto.FirstName, LastName = userDto.LastName, DateOfBirth = userDto.DateOfBirth, EmailAddress = userDto.EmailAddress, CapitalizedEmailAddress = userDto.EmailAddress.ToUpper(), RoleId = userDto.Role.Id, Password = password, Salt = salt };
            var sql = "INSERT INTO [dbo].[Users]([FirstName], [LastName], [DateOfBirth], [EmailAddress], [CapitalizedEmailAddress], [RoleId], [Password], [Salt]) VALUES(@FirstName, @LastName, @DateOfBirth, @EmailAddress, @CapitalizedEmailAddress, @RoleId, @Password, @Salt)";

            return await SqlMapper.ExecuteAsync(_connectionFactory.GetConnection, sql, parameters);

        }

        public async Task<bool> DoesEmailAddressExistsAsync(string emailAddress)
        {
            var parameters = new { CapitalizedEmailAddress = emailAddress.ToUpper() };
            var sql = "SELECT COUNT(1) FROM [dbo].[Users] WHERE CapitalizedEmailAddress = @CapitalizedEmailAddress";

            return await SqlMapper.ExecuteScalarAsync<bool>(_connectionFactory.GetConnection, sql, parameters);
        }

        public async Task<bool> DoesPasswordsMatchAsync(string emailAddress, string password)
        {
            var parameters = new { CapitalizedEmailAddress = emailAddress.ToUpper(), Password = password };
            var sql = "SELECT COUNT(1) FROM [dbo].[Users] WHERE CapitalizedEmailAddress = @CapitalizedEmailAddress AND Password = @Password";

            return await SqlMapper.ExecuteScalarAsync<bool>(_connectionFactory.GetConnection, sql, parameters);
        }

        public async Task<IEnumerable<UserDto>> GetAllStudentsAsync(Guid roleId)
        {
            var parameters = new { RoleId = roleId };
            var sql = @"SELECT u.*, r.* FROM [dbo].[Users] u
                            LEFT JOIN [dbo].[Roles] r
                            ON u.RoleId = r.Id
                            WHERE u.RoleId = @RoleId;";

            return await SqlMapper.QueryAsync<UserDto, RoleDto, UserDto>(_connectionFactory.GetConnection, sql,
                (user, role) => { user.Role = role; return user; }, parameters);
        }

        public async Task<IEnumerable<UserDto>> GetAllStudentsByInstructorAsync(Guid instructorId)
        {
            var sql = "SELECT u.*, r.* FROM [dbo].[InstructorStudents] i INNER JOIN [dbo].[Users] u ON i.StudentId = u.Id INNER JOIN [dbo].[Roles] r ON u.RoleId = r.Id";

            return await SqlMapper.QueryAsync<UserDto, RoleDto, UserDto>(_connectionFactory.GetConnection, sql,
                (student, role) => { student.Role = role; return student; },
                splitOn: "Id,Id");
        }

        public async Task<string> GetSaltByEmailaddressAsync(string emailAddress)
        {
            var parameters = new { CapitalizedEmailAddress = emailAddress.ToUpper() };
            var sql = "SELECT [Salt] FROM [dbo].[Users] WHERE CapitalizedEmailAddress = @CapitalizedEmailAddress";

            return await SqlMapper.QueryFirstOrDefaultAsync<string>(_connectionFactory.GetConnection, sql, parameters);
        }

        public async Task<UserDto> GetUserByEmailAddressAsync(string emailAddress)
        {
            var parameters = new { CapitalizedEmailAddress = emailAddress.ToUpper() };
            var sql = @"SELECT u.*, r.* FROM [dbo].[Users] u
                            LEFT JOIN [dbo].[Roles] r
                            ON u.RoleId = r.Id
                            WHERE u.CapitalizedEmailAddress = @CapitalizedEmailAddress;";

            var result = await SqlMapper.QueryAsync<UserDto, RoleDto, UserDto>(_connectionFactory.GetConnection, sql,
                (user, role) => { user.Role = role; return user; }, parameters);

            return result.FirstOrDefault();
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var parameters = new { Id = id };
            var sql = @"SELECT u.*, r.* FROM [dbo].[Users] u
                            LEFT JOIN [dbo].[Roles] r
                            ON u.RoleId = r.Id
                            WHERE u.Id = @Id;";

            var result = await SqlMapper.QueryAsync<UserDto, RoleDto, UserDto>(_connectionFactory.GetConnection, sql,
                (user, role) => { user.Role = role; return user; }, parameters);

            return result.FirstOrDefault();
        }

        public async Task<int> RemoveStudentFromInstructorAsync(Guid instructorId, Guid studentId)
        {
            var parameters = new { InstructorId = instructorId, StudentId = studentId };
            var sql = "DELETE FROM [dbo].[InstructorStudents] WHERE InstructorId = @InstructorId AND StudentID = @StudentId";

            return await SqlMapper.ExecuteAsync(_connectionFactory.GetConnection, sql, parameters);
        }

        public async Task<int> UpdateUserAsync(UserDto userDto)
        {
            var parameters = new { Id = userDto.Id, FirstName = userDto.FirstName, LastName = userDto.LastName, DateOfBirth = userDto.DateOfBirth, City = userDto.City, StreetNAme = userDto.StreetName, HouseNumber = userDto.HouseNumber };
            var sql = "UPDATE [dbo].[Users] SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, City = @City, StreetName = @StreetName, HouseNumber = @HouseNumber WHERE Id = @Id; ";

            return await SqlMapper.ExecuteAsync(_connectionFactory.GetConnection, sql, parameters);
        }
    }
}
