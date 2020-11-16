using RijlesPlanner.IDataAccessLayer.Dtos;

namespace RijlesPlanner.IDataAccessLayer
{
    public interface IUserContainerDal
    {
        public int CreateUser(UserDto userDto, string salt, string password);
        public UserDto GetUserById(int id);
        public UserDto GetUserByEmailAddress(string emailAddress);
    }
}
