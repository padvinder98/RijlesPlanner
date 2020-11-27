using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RijlesPlanner.ApplicationCore.Interfaces;
using RijlesPlanner.ApplicationCore.Models;
using RijlesPlanner.IDataAccessLayer;
using RijlesPlanner.IDataAccessLayer.Dtos;

namespace RijlesPlanner.ApplicationCore.Containers
{
    public class LessonContainer : ILessonContainer
    {
        private readonly ILessonContainerDal _lessonContainerDal;
        private readonly IUserContainerDal _userContainerDal;

        public LessonContainer(ILessonContainerDal lessonContainerDal, IUserContainerDal userContainerDal)
        {
            _lessonContainerDal = lessonContainerDal;
            _userContainerDal = userContainerDal;
        }

        public async Task<int> CreateNewLessonAsync(Lesson lesson)
        {
            var userDto = await _userContainerDal.GetUserByEmailAddressAsync(lesson.Instructor.EmailAddress);

            int count = await _lessonContainerDal.CreateNewLessonAsync(new LessonDto(lesson.Title, lesson.Description, lesson.StartDate, lesson.EndDate, userDto));

            return count;
        }

        public Task<int> DeleteLessonAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Lesson> FindLessonByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Lesson>> GetAllLessonsAsync()
        {
            var result = await _lessonContainerDal.GetAllLessonsAsync();

            return new List<Lesson>();
        }
    }
}
