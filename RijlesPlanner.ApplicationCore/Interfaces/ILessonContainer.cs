using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RijlesPlanner.ApplicationCore.Models;

namespace RijlesPlanner.ApplicationCore.Interfaces
{
    public interface ILessonContainer
    {
        public Task<List<Lesson>> GetAllLessonsAsync();
        public Task<int> CreateNewLessonAsync(Lesson lesson);
        public Task<int> DeleteLessonAsync(Guid id);
        public Task<Lesson> GetLessonByIdAsync(Guid id);
        public Task<List<Lesson>> GetLessonsByUserIdAsync(Guid userId);
    }
}
