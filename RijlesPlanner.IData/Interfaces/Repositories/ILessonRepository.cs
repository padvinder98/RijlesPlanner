using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RijlesPlanner.IData.Dtos;

namespace RijlesPlanner.IData.Interfaces
{
    public interface ILessonRepository
    {
        public Task<IEnumerable<LessonDto>> GetAllLessonsAsync();
        public Task<LessonDto> GetLessonByIdAsync(Guid id);
        public Task<IEnumerable<LessonDto>> GetLessonsByUserIdAsync(Guid userId);
        public Task<int> CreateNewLessonAsync(LessonDto lessonDto);
        public Task<int> DeleteLessonAsync(Guid id);
    }
}
