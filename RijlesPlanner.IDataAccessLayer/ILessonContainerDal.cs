using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RijlesPlanner.IDataAccessLayer.Dtos;

namespace RijlesPlanner.IDataAccessLayer
{
    public interface ILessonContainerDal
    {
        public Task<List<LessonDto>> GetAllLessonsAsync();
        public Task<List<LessonDto>> GetLessonsByUserIdAsync(Guid userId);
        public Task<int> CreateNewLessonAsync(LessonDto lessonDto);
        public Task<LessonDto> FindLessonByIdAsync(string id);
        public Task<int> DeleteLessonAsync(string id);
    }
}
