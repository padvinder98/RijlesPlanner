using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RijlesPlanner.ApplicationCore.Interfaces;
using RijlesPlanner.UI.MVC.ViewModels.DashboardViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RijlesPlanner.UI.MVC.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class StudentController : Controller
    {
        private readonly IUserContainer _userContainer;

        public StudentController(IUserContainer userContainer)
        {
            _userContainer = userContainer;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentInfoPartialView(Guid id)
        {
            var student = await _userContainer.GetUserByIdAsync(id);

            var model = new StudentInfoViewModel { Student = student };

            return PartialView("~/views/Shared/PartialViews/StudentPartialViews/_StudentInfoPartialView.cshtml", model);
        }
    }
}
