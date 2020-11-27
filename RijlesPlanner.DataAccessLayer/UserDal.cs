using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using RijlesPlanner.DataAccessLayer.Connection;
using RijlesPlanner.IDataAccessLayer;
using RijlesPlanner.IDataAccessLayer.Dtos;

namespace RijlesPlanner.DataAccessLayer
{
    public class UserDal : IUserContainerDal
    {
        public async Task<int> CreateNewUserAsync(UserDto userDto, string password, string salt)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { FirstName = userDto.FirstName, LastName = userDto.LastName, DateOfBirth = userDto.DateOfBirth, EmailAddress = userDto.EmailAddress, CapitalizedEmailAddress = userDto.EmailAddress.ToUpper(), RoleId = userDto.RoleDto.Id, Password = password, Salt = salt };
                var sql = "INSERT INTO [dbo].[Users]([FirstName], [LastName], [DateOfBirth], [EmailAddress], [CapitalizedEmailAddress], [RoleId], [Password], [Salt]) VALUES(@FirstName, @LastName, @DateOfBirth, @EmailAddress, @CapitalizedEmailAddress, @RoleId, @Password, @Salt)";

                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<bool> DoesEmailAddressExistsAsync(string emailAddress)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { CapitalizedEmailAddress = emailAddress.ToUpper() };
                var sql = "SELECT COUNT(1) FROM [dbo].[Users] WHERE CapitalizedEmailAddress = @CapitalizedEmailAddress";

                return await connection.ExecuteScalarAsync<bool>(sql, parameters);
            }
        }

        public async Task<bool> DoesPasswordsMatchAsync(string emailAddress, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { CapitalizedEmailAddress = emailAddress.ToUpper(), Password = password };
                var sql = "SELECT COUNT(1) FROM [dbo].[Users] WHERE CapitalizedEmailAddress = @CapitalizedEmailAddress AND Password = @Password";

                return await connection.ExecuteScalarAsync<bool>(sql, parameters);
            }
        }

        public async Task<List<UserDto>> GetAllStudents(Guid roleId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { RoleId = roleId };
                var sql = @"SELECT u.*, r.* FROM [dbo].[Users] u
                            LEFT JOIN [dbo].[Roles] r
                            ON u.RoleId = r.Id
                            WHERE u.RoleId = @RoleId;";

                var user = await connection.QueryAsync<UserDto, RoleDto, UserDto>(sql,
                    (user, role) => { user.RoleDto = role; return user; }, parameters);

                return user.ToList();
            }
        }

        public async Task<string> GetSaltByEmailaddressAsync(string emailAddress)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { CapitalizedEmailAddress = emailAddress.ToUpper() };
                var sql = "SELECT [Salt] FROM [dbo].[Users] WHERE CapitalizedEmailAddress = @CapitalizedEmailAddress";

                return await connection.QueryFirstOrDefaultAsync<string>(sql, parameters);
            }
        }

        public async Task<UserDto> GetUserByEmailAddressAsync(string emailAddress)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { CapitalizedEmailAddress = emailAddress.ToUpper() };
                var sql = @"SELECT u.*, r.* FROM [dbo].[Users] u
                            LEFT JOIN [dbo].[Roles] r
                            ON u.RoleId = r.Id
                            WHERE u.CapitalizedEmailAddress = @CapitalizedEmailAddress;";

                var user = await connection.QueryAsync<UserDto, RoleDto, UserDto>(sql,
                    (user, role) => { user.RoleDto = role; return user; }, parameters);

                return user.FirstOrDefault();
            }
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { Id = id };
                var sql = @"SELECT u.*, r.* FROM [dbo].[Users] u
                            LEFT JOIN [dbo].[Roles] r
                            ON u.RoleId = r.Id
                            WHERE u.Id = @Id;";

                var user = await connection.QueryAsync<UserDto, RoleDto, UserDto>(sql,
                    (user, role) => { user.RoleDto = role; return user; }, parameters);

                return user.FirstOrDefault();
            }
        }

        public async Task<int> UpdateUserAsync(UserDto userDto)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters= new { Id = userDto.Id, FirstName = userDto.FirstName, LastName= userDto.LastName, DateOfBirth = userDto.DateOfBirth, City = userDto.City, StreetNAme = userDto.StreetName, HouseNumber = userDto.HouseNumber };
                var sql = "UPDATE [dbo].[Users] SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, City = @City, StreetName = @StreetName, HouseNumber = @HouseNumber WHERE Id = @Id; ";

                return await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
