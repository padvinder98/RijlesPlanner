using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RijlesPlanner.ApplicationCore.Interfaces;
using RijlesPlanner.ApplicationCore.Models;
using RijlesPlanner.UI.MVC.ViewModels.AccountViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RijlesPlanner.UI.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IRoleContainer _roleContainer;
        private readonly IUserContainer _userContainer;

        public AccountController(IRoleContainer roleContainer, IUserContainer userContainer)
        {
            _roleContainer = roleContainer;
            _userContainer = userContainer;
        }

        // GET: Account/Register
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleContainer.GetRoleByName("Student");
                var user = new User(model.FirstName, model.LastName, model.DateOfBirth, model.EmailAddress, role);

                var result = await _userContainer.CreateNewUserAsync(user, model.Password);

                if (result.IsSucceedded)
                {
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View();
            }

            return View();
        }

        // GET: Account/Login
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        // POST: Account/Login
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _userContainer.DoesEmailAddressExistsAsync(model.EmailAddress) && await _userContainer.DoesPasswordsMatchAsync(model.EmailAddress, model.Password))
                {
                    var user = await _userContainer.GetUserByEmailAddressAsync(model.EmailAddress);

                    // Create the identity
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.EmailAddress));
                    identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
                    identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
                    identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.Name));

                    // Sign in
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Dashboard");
                }

                ModelState.AddModelError(string.Empty, "Invalid login.");

                return View();
            }

            return View();
        }

        // GET: Account/LogOut
        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        // GET: Account/Profile
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userContainer.GetUserByEmailAddressAsync(User.Identity.Name);

            return View(new ProfileViewModel { FirstName = user.FirstName, LastName = user.LastName, DateOfBirth = user.DateOfBirth, EmailAddress = user.EmailAddress, StreetName = user.StreetName, City = user.City, HouseNumber = user.HouseNumber });
        }

        // POST: Account/Profile
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            var user = await _userContainer.GetUserByEmailAddressAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                user.Update(model.FirstName, model.LastName, model.DateOfBirth, model.City, model.StreetName, model.HouseNumber);

                await _userContainer.UpdateUserAsync(user);

                return View(new ProfileViewModel { FirstName = user.FirstName, LastName = user.LastName, DateOfBirth = user.DateOfBirth, EmailAddress = user.EmailAddress, StreetName = user.StreetName, City = user.City, HouseNumber = user.HouseNumber });
            }

            return View(new ProfileViewModel { FirstName = user.FirstName, LastName = user.LastName, DateOfBirth = user.DateOfBirth, EmailAddress = user.EmailAddress, StreetName = user.StreetName, City = user.City, HouseNumber = user.HouseNumber });
        }
    }
}
