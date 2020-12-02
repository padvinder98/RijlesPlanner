using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RijlesPlanner.ApplicationCore.Interfaces;
using RijlesPlanner.ApplicationCore.Models;
using RijlesPlanner.UI.MVC.ViewModels.LessonViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RijlesPlanner.UI.MVC.Controllers
{
    public class LessonController : Controller
    {
        private readonly ILessonContainer _lessonContainer;
        private readonly IUserContainer _userContainer;
        private readonly IRoleContainer _roleContainer;

        public LessonController(ILessonContainer lessonContainer, IUserContainer userContainer, IRoleContainer roleContainer)
        {
            _lessonContainer = lessonContainer;
            _userContainer = userContainer;
            _roleContainer = roleContainer;
        }

        // GET: Student/GetAddLessonPartialView
        [HttpGet]
        public async Task<IActionResult> GetAddLessonPartialView()
        {
            var instructor = await _userContainer.GetUserByEmailAddressAsync(User.Identity.Name);
            var students = await _userContainer.GetAllStudentsByInstructorAsync(instructor.Id);

            var selectList = students.Select(s =>
                                new SelectListItem
                                {
                                    Value = s.Id.ToString(),
                                    Text = s.FirstName + " " + s.LastName
                                }).ToList();

            var dt = DateTime.Now;
            var model = new AddLessonViewModel { Students = selectList, StartDate = dt.Date.AddHours(dt.Hour).AddMinutes(dt.Minute), EndDate = dt.Date.AddHours(dt.Hour).AddMinutes(dt.Minute) };

            return PartialView("~/views/Shared/PartialViews/LessonPartialViews/_AddLessonPartialView.cshtml", model);
        }

        // GET: Student/GetLessonInfoPartialView
        [HttpGet]
        public async Task<IActionResult> GetLessonInfoPartialView(Guid id)
        {
            var lesson = await _lessonContainer.GetLessonByIdAsync(id);

            var model = new LessonViewModel { Id = lesson.Id, Title = lesson.Title, Description = lesson.Description, End = lesson.EndDate, Start = lesson.StartDate};

            return PartialView("~/views/Shared/PartialViews/LessonPartialViews/_LessonInfoPartialView.cshtml", model);
        }

        // GET: Student/GetAllLessons
        [HttpGet]
        public async Task<JsonResult> GetAllLessons()
        {
            var user = await _userContainer.GetUserByEmailAddressAsync(User.Identity.Name);

            var result = await _lessonContainer.GetLessonsByUserIdAsync(user.Id);

            var data = new List<LessonViewModel>();

            foreach (var item in result)
            {
                data.Add(new LessonViewModel { Id = item.Id, Title = item.Title, Description = item.Description, Start = item.StartDate, End = item.EndDate });
            }

            return Json(data);
        }

        // POST: Student/AddLesson
        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<JsonResult> AddLesson(AddLessonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var instructor = await _userContainer.GetUserByEmailAddressAsync(User.Identity.Name);
                var student = await _userContainer.GetUserByIdAsync(Guid.Parse(model.StudentId));

                if (student.HouseNumber == null || student.City == null || student.StreetName == null)
                {
                    return Json(new { success = true, responseText = "De gebruiker heeft nog geen adres gegevens ingevoerd." });
                }

                var newLesson = new Lesson(model.Title, model.Description, model.StartDate, model.EndDate, instructor, student);

                int result = await _lessonContainer.CreateNewLessonAsync(newLesson);

                if (result > 0)
                {
                    return Json(new { success = true, responseText = "De les is gepland." });
                }

                return Json(new { success = false, responseText = "Er ging iets mis." });
            }

            return Json(new { success = false, responseText = "Niet alle velden zijn correct ingevuld." });
        }

        // POST: Schedule/DeleteLesson()
        [HttpPost]
        public async Task<IActionResult> DeleteLesson(Guid id)
        {
            var result = await _lessonContainer.GetLessonByIdAsync(id);

            if (result == null)
            {
                return Json(new { success = false, responseText = "Er ging iets mis." });
            }

            int rowsDeleted = await _lessonContainer.DeleteLessonAsync(id);

            if (rowsDeleted <= 0)
            {
                return Json(new { success = false, responseText = "Er ging iets mis." });
            }

            return Json(new { success = true });
        }
    }
}
