using System;
using System.Threading.Tasks;
using RijlesPlanner.IData.Dtos;

namespace RijlesPlanner.IData.Interfaces
{
    public interface IRoleRepository
    {
        public Task<RoleDto> GetRoleByNameAsync(string roleName);
    }
}
