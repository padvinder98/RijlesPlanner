using RijlesPlanner.IDataAccessLayer.Dtos;

namespace RijlesPlanner.IDataAccessLayer
{
    public interface IUserContainer
    {
        public UserDto GetUserById(int id);
        public UserDto GetUserByEmailAddress(string emailAddress);
    }
}
