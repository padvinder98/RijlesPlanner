using System;
namespace RijlesPlanner.IData.Dtos
{
    public class LessonDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public UserDto Instructor { get; set; }
        public UserDto Student { get; set; }

        public LessonDto()
        {

        }

        public LessonDto(string title, string description, DateTime startDate, DateTime endDate, UserDto instructor, UserDto student)
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
