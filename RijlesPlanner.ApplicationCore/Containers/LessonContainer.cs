using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RijlesPlanner.ApplicationCore.Interfaces;
using RijlesPlanner.ApplicationCore.Models;
using RijlesPlanner.IData.Dtos;
using RijlesPlanner.IData.Interfaces;

namespace RijlesPlanner.ApplicationCore.Containers
{
    public class LessonContainer : ILessonContainer
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IUserRepository _userRepository;

        public LessonContainer(ILessonRepository lessenRepository, IUserRepository userRepository)
        {
            _lessonRepository = lessenRepository;
            _userRepository = userRepository;
        }

        public async Task<int> CreateNewLessonAsync(Lesson lesson)
        {
            var instructorDto = await _userRepository.GetUserByEmailAddressAsync(lesson.Instructor.EmailAddress);
            var studentDto = await _userRepository.GetUserByEmailAddressAsync(lesson.Student.EmailAddress);

            int count = await _lessonRepository.CreateNewLessonAsync(new LessonDto(lesson.Title, lesson.Description, lesson.StartDate, lesson.EndDate, instructorDto, studentDto));

            return count;
        }

        public async Task<int> DeleteLessonAsync(Guid id)
        {
            return await _lessonRepository.DeleteLessonAsync(id);
        }

        public async Task<List<Lesson>> GetAllLessonsAsync()
        {
            var result = await _lessonRepository.GetAllLessonsAsync();

            return result.Select(l => new Lesson(l)).ToList();
        }

        public async Task<Lesson> GetLessonByIdAsync(Guid id)
        {
            var result = await _lessonRepository.GetLessonByIdAsync(id);

            return new Lesson(result);
        }

        public async Task<List<Lesson>> GetLessonsByUserIdAsync(Guid userId)
        {
            var result = await _lessonRepository.GetLessonsByUserIdAsync(userId);

            return result.Select(l => new Lesson(l)).ToList();
        }
    }
}
