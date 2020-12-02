using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RijlesPlanner.ApplicationCore.Interfaces;
using RijlesPlanner.ApplicationCore.Models;
using RijlesPlanner.UI.MVC.ViewModels.DashboardViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RijlesPlanner.UI.MVC.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ILessonContainer _lessonContainer;
        private readonly IUserContainer _userContainer;
        private readonly IRoleContainer _roleContainer;

        public DashboardController(ILessonContainer lessonContainer, IUserContainer userContainer, IRoleContainer roleContainer)
        {
            _lessonContainer = lessonContainer;
            _userContainer = userContainer;
            _roleContainer = roleContainer;
        }

        // GET: Dashboard/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userContainer.GetUserByEmailAddressAsync(User.Identity.Name);

            var result = await _lessonContainer.GetLessonsByUserIdAsync(user.Id);

            return View(result);
        }

        // GET: Dashboard/Shedule
        [HttpGet]
        public IActionResult Shedule()
        {
            return View();
        }

        // GET: Dashboard/Students
        [HttpGet]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> Students()
        {
            var role = await _roleContainer.GetRoleByName("Student");
            var instructor = await _userContainer.GetUserByEmailAddressAsync(User.Identity.Name);

            var allStudents = await _userContainer.GetAllStudents(role.Id);
            var instructorStudents = await _userContainer.GetAllStudentsByInstructorAsync(instructor.Id);

            var model = new StudentsViewModel { AllStudents = allStudents, InstructorStudents = instructorStudents };

            return View(model);
        }

        // POST: Dashboard/AddStudentToInstuctor
        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> AddStudentToInstuctor(Guid studentId)
        {
            var user = await _userContainer.GetUserByEmailAddressAsync(User.Identity.Name);

            var result = await _userContainer.AddStudentToInstructorAsync(user.Id, studentId);

            if (result > 0)
            {
                return Json(new { success = true, responseText = "De student is toegevoegd." });
            }

            return Json(new { success = false, responseText = "Er ging iets mis." });
        }

        // POST: Dashboard/RemoveStudentFromInstructor
        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> RemoveStudentFromInstructor(Guid studentId)
        {
            var user = await _userContainer.GetUserByEmailAddressAsync(User.Identity.Name);

            var result = await _userContainer.RemoveStudentFromInstructorAsync(user.Id, studentId);

            if (result > 0)
            {
                return Json(new { success = true, responseText = "De student is verwijderd." });
            }

            return Json(new { success = false, responseText = "Er ging iets mis." });
        }
    }        
}
