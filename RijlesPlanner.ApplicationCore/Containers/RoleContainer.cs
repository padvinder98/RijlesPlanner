using System;
using System.Threading.Tasks;
using RijlesPlanner.ApplicationCore.Interfaces;
using RijlesPlanner.ApplicationCore.Models;
using RijlesPlanner.IData.Interfaces;

namespace RijlesPlanner.ApplicationCore.Containers
{
    public class RoleContainer : IRoleContainer
    {
        private readonly IRoleRepository _roleRepository;

        public RoleContainer(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            return new Role(await _roleRepository.GetRoleByNameAsync(roleName));
        }
    }
}
