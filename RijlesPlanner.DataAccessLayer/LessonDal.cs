using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using RijlesPlanner.DataAccessLayer.Connection;
using RijlesPlanner.IDataAccessLayer;
using RijlesPlanner.IDataAccessLayer.Dtos;

namespace RijlesPlanner.DataAccessLayer
{
    public class LessonDal : ILessonContainerDal
    {
        public async Task<int> CreateNewLessonAsync(LessonDto lessonDto)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { Title = lessonDto.Title, Description = lessonDto.Description, StartDate = lessonDto.StartDate, EndDate = lessonDto.EndDate, InstructorId = lessonDto.Instructor.Id };
                var sql = "INSERT INTO [dbo].[Lessons]([Title], [Description], [StartDate], [EndDate], [InstructorId]) VALUES(@Title, @Description, @StartDate, @EndDate, @InstructorId)";

                var result = await connection.QueryAsync<int>(sql, parameters);

                return 1;
            }
        }

        public Task<int> DeleteLessonAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<LessonDto> FindLessonByIdAsync(string id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { Id = id };
                var sql = "SELECT * FROM [dbo].[Lessons] WHERE Id = @Id";

                var result = await connection.QueryFirstOrDefaultAsync(sql, parameters);

                return result;
            }
        }

        public async Task<List<LessonDto>> GetAllLessonsAsync()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                try
                {
                    var sql = @"SELECT l.*, u.* FROM [dbo].[Lessons] l
                            LEFT JOIN [dbo].[Users] u
                            ON l.InstructorId = u.Id;";

                    var result = await connection.QueryAsync<LessonDto, UserDto, LessonDto>(sql,
                        (lesson, user) => { lesson.Instructor = user; return lesson; });

                    return result.ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
        }

        public async Task<List<LessonDto>> GetLessonsByUserIdAsync(Guid userId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                var parameters = new { Id = userId };
                var sql = "SELECT * FROM [dbo].[Lessons] WHERE Id = @Id";

                var result = await connection.QueryAsync<LessonDto>(sql, parameters);

                return result.ToList();
            }
        }
    }
}
