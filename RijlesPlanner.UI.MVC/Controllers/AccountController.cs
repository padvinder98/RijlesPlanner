using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RijlesPlanner.ApplicationCore.Interfaces;
using RijlesPlanner.ApplicationCore.Models.User;
using RijlesPlanner.UI.MVC.ViewModels.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RijlesPlanner.UI.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserContainer _userContainer;

        public AccountController(IUserContainer userContainer)
        {
            _userContainer = userContainer;
        }

        // GET: Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User(model.FirtName, model.LastName, model.BirthDate, model.EmailAddress);

                var result = _userContainer.CreateUser(user, model.Password);

                if (result.IsSucceed)
                {
                    var principal = _userContainer.LoginUser(user);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Login", "Account");
                }

                ModelState.AddModelError(string.Empty, result.Error);

                return View(model);
            }

            return View();
        }

        // GET: Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userContainer.FindUserByEmail(model.EmailAddress);

                var result = _userContainer.LoginUserWithPassword(user, model.Password);

                if (result.IsSucceed)
                {
                    var principal = _userContainer.LoginUser(user);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, result.Error);

                return View();
            }

            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
