using System;
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

        public Lesson(string title, string description, DateTime startDate, DateTime endDate, User instructor)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Instructor = instructor;
        }
    }
}
