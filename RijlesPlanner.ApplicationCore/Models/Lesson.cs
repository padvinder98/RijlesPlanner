using System;
using RijlesPlanner.IData.Dtos;

namespace RijlesPlanner.ApplicationCore.Models
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User Instructor { get; set; }
        public User Student { get; set; }


        public Lesson(LessonDto lessonDto)
        {
            Id = lessonDto.Id;
            Title = lessonDto.Title;
            Description = lessonDto.Description;
            StartDate = lessonDto.StartDate;
            EndDate = lessonDto.EndDate;
            Instructor = new User(lessonDto.Instructor);
            Student = new User(lessonDto.Student);
        }
        
        public Lesson(string title, string description, DateTime startDate, DateTime endDate, User instructor, User student)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Instructor = instructor;
            Student = student;
        }
    }
}
