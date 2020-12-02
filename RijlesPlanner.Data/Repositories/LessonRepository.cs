using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using RijlesPlanner.IData.Dtos;
using RijlesPlanner.IData.Interfaces;
using RijlesPlanner.IData.Interfaces.ConnectionFactory;

namespace RijlesPlanner.Data.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly IConnection _connectionFactory;

        public LessonRepository(IConnection connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> CreateNewLessonAsync(LessonDto lessonDto)
        {
            var parameters = new { Title = lessonDto.Title, Description = lessonDto.Description, StartDate = lessonDto.StartDate, EndDate = lessonDto.EndDate, InstructorId = lessonDto.Instructor.Id, StudentId = lessonDto.Student.Id };
            var sql = "INSERT INTO [dbo].[Lessons]([Title], [Description], [StartDate], [EndDate], [InstructorId], [StudentId]) VALUES(@Title, @Description, @StartDate, @EndDate, @InstructorId, @StudentId)";

            return await SqlMapper.ExecuteAsync(_connectionFactory.GetConnection, sql, parameters);
        }

        public async Task<int> DeleteLessonAsync(Guid id)
        {
            var parameters = new { Id = id };
            var sql = "DELETE FROM [dbo].[Lessons] WHERE Id = @Id";

            return await SqlMapper.ExecuteAsync(_connectionFactory.GetConnection, sql, parameters);
        }

        public async Task<IEnumerable<LessonDto>> GetAllLessonsAsync()
        {
            var sql = @"SELECT l.*, i.*, ir.*, s.*, sr.* FROM [dbo].[Lessons] l
                            INNER JOIN [dbo].[Users] i ON l.InstructorId = i.Id
                            INNER JOIN [dbo].[Roles] ir ON i.RoleId = ir.Id
                            INNER JOIN [dbo].[Users] s ON l.StudentId = s.Id
                            INNER JOIN [dbo].[Roles] sr ON s.RoleId = sr.Id";

            return await SqlMapper.QueryAsync<LessonDto, UserDto, RoleDto, UserDto, RoleDto, LessonDto>(_connectionFactory.GetConnection, sql,
                (lesson, instructor, roleInstructor, student, roleStudent) => { lesson.Instructor = instructor; instructor.Role = roleInstructor; lesson.Student = student; student.Role = roleStudent; return lesson; },
                splitOn: "Id,Id");
        }

        public async Task<LessonDto> GetLessonByIdAsync(Guid id)
        {
            var parameters = new { lessonId = id };
            var sql = @"SELECT l.*, i.*, ir.*, s.*, sr.* FROM [dbo].[Lessons] l
                            INNER JOIN [dbo].[Users] i ON l.InstructorId = i.Id
                            INNER JOIN [dbo].[Roles] ir ON i.RoleId = ir.Id
                            INNER JOIN [dbo].[Users] s ON l.StudentId = s.Id
                            INNER JOIN [dbo].[Roles] sr ON s.RoleId = sr.Id
                            WHERE l.Id = @lessonId";

            var result = await SqlMapper.QueryAsync<LessonDto, UserDto, RoleDto, UserDto, RoleDto, LessonDto>(_connectionFactory.GetConnection, sql,
                (lesson, instructor, roleInstructor, student, roleStudent) => { lesson.Instructor = instructor; instructor.Role = roleInstructor; lesson.Student = student; student.Role = roleStudent; return lesson; },
                parameters,
                splitOn: "Id,Id");

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<LessonDto>> GetLessonsByUserIdAsync(Guid userId)
        {
            var parameters = new { UserId = userId };
            var sql = @"SELECT l.*, i.*, ir.*, s.*, sr.* FROM [dbo].[Lessons] l
                            INNER JOIN [dbo].[Users] i ON l.InstructorId = i.Id
                            INNER JOIN [dbo].[Roles] ir ON i.RoleId = ir.Id
                            INNER JOIN [dbo].[Users] s ON l.StudentId = s.Id
                            INNER JOIN [dbo].[Roles] sr ON s.RoleId = sr.Id
                            WHERE InstructorId = @UserId OR StudentId = @UserId";

            return await SqlMapper.QueryAsync<LessonDto, UserDto, RoleDto, UserDto, RoleDto, LessonDto>(_connectionFactory.GetConnection, sql,
                (lesson, instructor, roleInstructor, student, roleStudent) => { lesson.Instructor = instructor; instructor.Role = roleInstructor; lesson.Student = student; student.Role = roleStudent; return lesson; },
                parameters,
                splitOn: "Id,Id");
        }
    }
}
