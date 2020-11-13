using Dapper;
using RijlesPlanner.DataAccessLayer.Connection;
using RijlesPlanner.IDataAccessLayer;
using RijlesPlanner.IDataAccessLayer.Dtos;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace RijlesPlanner.DataAccessLayer
{
    public class UserDal : IUserContainer
    {
        public UserDto GetUserByEmailAddress(string emailAddress)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { EmailAddress = emailAddress };
                var query = "Select [FirstName], [LastName], [EmailAddress], [BirthDate] FROM [dbo].[Users] WHERE EmailAddress == @EmailAddress";

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
