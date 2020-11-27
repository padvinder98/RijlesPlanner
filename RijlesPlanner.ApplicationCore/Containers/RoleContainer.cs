using System;
using System.Threading.Tasks;
using RijlesPlanner.ApplicationCore.Interfaces;
using RijlesPlanner.ApplicationCore.Models;
using RijlesPlanner.IDataAccessLayer;

namespace RijlesPlanner.ApplicationCore.Containers
{
    public class RoleContainer : IRoleContainer
    {
        private readonly IRoleContainerDal _roleContainerDal;

        public RoleContainer(IRoleContainerDal roleContainerDal)
        {
            _roleContainerDal = roleContainerDal;
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            return new Role(await _roleContainerDal.GetRoleByNameAsync(roleName));
        }
    }
}
