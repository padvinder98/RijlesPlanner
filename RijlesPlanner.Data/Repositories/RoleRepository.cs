using System;
using System.Threading.Tasks;
using Dapper;
using RijlesPlanner.IData.Dtos;
using RijlesPlanner.IData.Interfaces;
using RijlesPlanner.IData.Interfaces.ConnectionFactory;

namespace RijlesPlanner.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IConnection _connectionFactory;

        public RoleRepository(IConnection connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<RoleDto> GetRoleByNameAsync(string roleName)
        {
            var parameters = new { Name = roleName };
            var sql = "SELECT * FROM [dbo].[Roles] WHERE Name = @Name";

            return await SqlMapper.QueryFirstOrDefaultAsync<RoleDto>(_connectionFactory.GetConnection, sql, parameters);
        }
    }
}
