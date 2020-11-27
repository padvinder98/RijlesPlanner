using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RijlesPlanner.ApplicationCore.Interfaces;
using RijlesPlanner.ApplicationCore.Models;
using RijlesPlanner.ApplicationCore.ViewModels.DashboardViewModels;

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
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAllLessons()
        {
            var result = await _lessonContainer.GetAllLessonsAsync();

            return Json(new { Success = true }); ;
        }

        // POST: Dashboard/AddLesson
        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<JsonResult> AddLesson(AddLessonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userContainer.GetUserByEmailAddressAsync(User.Identity.Name);

                var lesson = new Lesson(model.Title, model.Description, model.StartDate, model.EndDate, user);

                int result = await _lessonContainer.CreateNewLessonAsync(lesson);

                if (result > 0)
                {
                    return Json(new { success = true, responseText = "De les is gepland." });
                }

                return Json(new { success = false, responseText = "Er ging iets mis." });
            }

            return Json(new { success = false, responseText = "Niet alle velden zijn correct ingevuld." });
        }

        // GET: Dashboard/Students
        [HttpGet]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> Students()
        {
            var role = await _roleContainer.GetRoleByName("Student");

            var allStudents = await _userContainer.GetAllStudents(role.Id);

            var model = new StudentsViewModel { AllStudents = allStudents };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentInfo(Guid id)
        {
            var user = await _userContainer.GetUserByIdAsync(id);

            var model = new StudentInfoViewModel { Student = user };

            return PartialView("~/views/Shared/PartialViews/DashboardPartialViews/_StudentInfoPartialView.cshtml", model);
        }
    }
}
