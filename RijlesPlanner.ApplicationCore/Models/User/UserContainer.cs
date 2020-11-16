using RijlesPlanner.ApplicationCore.Results;
using RijlesPlanner.IDataAccessLayer;
using System;

namespace RijlesPlanner.ApplicationCore.Models.User
{
    public class UserContainer
    {
        private readonly IUserContainerDal _userContainerDal;

        public UserContainer(IUserContainerDal userContainerDal)
        {
            _userContainerDal = userContainerDal;
        }

        public UserResult CreateUser(User user, string password)
        {
            if (DoesUserAlreadyExists(user.EmailAddress)) return new UserResult (  false, "User already exists." );

            return new UserResult(true);
        }

        public User FindUserById(int Id)
        {
            throw new NotImplementedException();
        }

        public User FindUserByEmail(string emailAddress)
        {
            throw new NotImplementedException();
        }

        private bool DoesUserAlreadyExists(string emailAddress)
        {
            if (FindUserByEmail(emailAddress) != null) return true;

            return false;
        }
    }
}
