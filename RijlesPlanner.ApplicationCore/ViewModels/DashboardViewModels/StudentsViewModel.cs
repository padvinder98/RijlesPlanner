using System;
using System.Collections.Generic;
using RijlesPlanner.ApplicationCore.Models;

namespace RijlesPlanner.ApplicationCore.ViewModels.DashboardViewModels
{
    public class StudentsViewModel
    {
        public List<User> AllStudents { get; set; }
        public List<User> InstructorStudents { get; set; }

        public StudentsViewModel()
        {
            AllStudents = new List<User>();
            InstructorStudents = new List<User>();
        }
    }
}
