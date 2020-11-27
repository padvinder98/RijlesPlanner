using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using RijlesPlanner.DataAccessLayer.Connection;
using RijlesPlanner.IDataAccessLayer;
using RijlesPlanner.IDataAccessLayer.Dtos;

namespace RijlesPlanner.DataAccessLayer
{
    public class RoleDal : IRoleContainerDal
    {
        public async Task<RoleDto> GetRoleByNameAsync(string roleName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { Name = roleName };
                var sql = "SELECT * FROM [dbo].[Roles] WHERE Name = @Name";

                return await connection.QueryFirstOrDefaultAsync<RoleDto>(sql, parameters);
            }
        }
    }
}
