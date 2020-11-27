using System.Threading.Tasks;
using RijlesPlanner.IDataAccessLayer.Dtos;

namespace RijlesPlanner.IDataAccessLayer
{
    public interface IRoleContainerDal
    {
        public Task<RoleDto> GetRoleByNameAsync(string roleName);
    }
}
