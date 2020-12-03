using System;
using System.Threading.Tasks;
using RijlesPlanner.ApplicationCore.Models;

namespace RijlesPlanner.ApplicationCore.Interfaces
{
    public interface IRoleContainer
    {
        public Task<Role> GetRoleByName(string roleName);
    }
}
