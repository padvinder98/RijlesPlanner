
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using RijlesPlanner.ApplicationCore.Interfaces;
using RijlesPlanner.ApplicationCore.Results;
using RijlesPlanner.ApplicationCore.Services;
using RijlesPlanner.IDataAccessLayer;
using RijlesPlanner.IDataAccessLayer.Dtos;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RijlesPlanner.ApplicationCore.Models.User
{
    public class UserContainer : IUserContainer
    {
        private readonly IUserContainerDal _userContainerDal;

        public UserContainer(IUserContainerDal userContainerDal)
        {
            _userContainerDal = userContainerDal;
        }

        public UserResult CreateUser(User user, string password)
        {
            if (DoesUserAlreadyExists(user.EmailAddress)) return new UserResult (  false, "User already exists." );

            var salt = HashingService.GetSalt();
            var hashedPassword = HashingService.HashPassword(salt, password);

            if (_userContainerDal.CreateUser(new UserDto (user.FirstName, user.LastName, user.BirthDate, user.EmailAddress ), salt, hashedPassword) == 1) 
            {
                return new UserResult(true);
            }

            return new UserResult(false, "Something went wrong.");
        }

        public User FindUserById(int Id)
        {
            throw new NotImplementedException();
        }

        public User FindUserByEmail(string emailAddress)
        {
            var result = _userContainerDal.GetUserByEmailAddress(emailAddress);

            if (result == null)
            {
                return null;
            }

            return new User(result.FirstName, result.LastName, result.BirthDate, result.EmailAddress);
        }

        private bool DoesUserAlreadyExists(string emailAddress)
        {
            if (FindUserByEmail(emailAddress) != null) return true;

            return false;
        }

        public ClaimsPrincipal LoginUser(User user)
        {
            // Create the identity
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.EmailAddress));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));

            // Sign in
            var principal = new ClaimsPrincipal(identity);

            return principal;

        }

        public UserResult LoginUserWithPassword(User user, string password)
        {
            if (user != null)
            {
                var salt = GetSaltByEmailAddress(user.EmailAddress);
                var hashedPassword = HashingService.HashPassword(salt, password);
                bool doMatch = DoPasswordsMatch(user.EmailAddress, hashedPassword);

                if (doMatch) { return new UserResult(true); };
            }

            return new UserResult(false, "Invalid login atempt.");
        }

        public string GetSaltByEmailAddress(string emailAddress)
        {
            return _userContainerDal.GetSaltByEamilAddress(emailAddress);
        }

        private bool DoPasswordsMatch(string emailAddress, string password)
        {
            return _userContainerDal.DoPasswordsMatch(emailAddress, password);
        }
    }
}
