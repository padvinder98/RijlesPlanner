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
        public Task<Lesson> FindLessonByIdAsync(string id);
        public Task<int> DeleteLessonAsync(string id);
    }
}
