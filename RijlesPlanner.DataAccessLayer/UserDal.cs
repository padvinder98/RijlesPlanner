using Dapper;
using RijlesPlanner.DataAccessLayer.Connection;
using RijlesPlanner.IDataAccessLayer;
using RijlesPlanner.IDataAccessLayer.Dtos;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace RijlesPlanner.DataAccessLayer
{
    public class UserDal : IUserContainerDal
    {
        public int CreateUser(UserDto userDto, string salt, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { Id = Guid.NewGuid(), FirstName = userDto.FirstName, LastName = userDto.LastName, EmailAddress = userDto.EmailAddress, BirthDate = userDto.BirthDate, Salt = salt, Password = password, CreateDate = DateTime.Now };
                var query = "INSERT INTO [dbo].[Users]([Id], [FirstName], [LastName], [EmailAddress], [BirthDate], [Salt], [Password], [CreateDate]) VALUES(@Id, @FirstName, @LastName, @EmailAddress, @BirthDate, @Salt, @Password, @CreateDate)";

                return connection.Execute(query, parameters);
            }
        }

        public bool DoPasswordsMatch(string emailAddress, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { EmailAddress = emailAddress, Password = password };
                var query = "SELECT count(1) FROM [dbo].[Users] WhERE EmailAddress = @EmailAddress AND Password = @Password";

                return connection.ExecuteScalar<bool>(query, parameters);
            }
        }

        public string GetSaltByEamilAddress(string emailAddress)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { emailAddress = emailAddress };
                var query = "SELECT [Salt] FROM [dbo].[users] WHERE EmailAddress = @EmailAddress";

                return connection.Query<string>(query, parameters).FirstOrDefault();
            }
        }

        public UserDto GetUserByEmailAddress(string emailAddress)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { EmailAddress = emailAddress };
                var query = "SELECT [Id], [FirstName], [LastName], [EmailAddress], [BirthDate] FROM [dbo].[Users] WHERE EmailAddress = @EmailAddress";

                return connection.Query<UserDto>(query, parameters).FirstOrDefault();
            }
        }

        public UserDto GetUserById(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { Id = id };
                var query = "Select [FirstName], [LastName], [EmailAddress], [BirthDate] FROM [dbo].[Users] WHERE Id == @Id";

                return connection.Query<UserDto>(query, parameters).FirstOrDefault();
            }
        }
    }
}
