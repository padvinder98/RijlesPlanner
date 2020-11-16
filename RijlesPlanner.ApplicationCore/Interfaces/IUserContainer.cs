using RijlesPlanner.ApplicationCore.Models.User;
using RijlesPlanner.ApplicationCore.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RijlesPlanner.ApplicationCore.Interfaces
{
    public interface IUserContainer
    {
        public UserResult CreateUser(User user, string password);
        public User FindUserById(int Id);
        public User FindUserByEmail(string emailAddress);
        public ClaimsPrincipal LoginUser(User user);
        public UserResult LoginUserWithPassword(User user, string password);
    }
}
