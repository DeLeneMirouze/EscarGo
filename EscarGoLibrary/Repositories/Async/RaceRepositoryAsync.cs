#region using

using EscarGoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace EscarGoLibrary.Repositories.Async
{
    public class RaceRepositoryAsync : BaseDataRepository, IRaceRepositoryAsync
    {
        #region Constructeur
        public RaceRepositoryAsync(EscarGoContext context) : base(context)
        {
        }
        #endregion

        #region CreateRequest (private)
        private IQueryable<Course> CreateRequest(int recordsPerPage, int currentPage)
        {
            var query = Context.Courses
               .Where(c => c.Date >= DateTime.UtcNow)
               .OrderByDescending(c => c.Date)
               .ThenBy(c => c.Pays)
               .ThenBy(c => c.Label)
               .Skip(currentPage * recordsPerPage);

            if (recordsPerPage > 0)
            {
                query = query.Take(recordsPerPage);
            }

            return query;
        } 
        #endregion

        #region GetRaces
        public async Task<List<Course>> GetRacesAsync(int recordsPerPage, int currentPage)
        {
            var req = CreateRequest(recordsPerPage, currentPage);
            List<Course> races = await SqlAzureRetry.ExecuteAsync(async () => await req.ToListAsync());

            if (races.Count == 0 && currentPage > 0)
            {
                currentPage--;
                req = CreateRequest(recordsPerPage, currentPage);
                races = await req.ToListAsync();
            }

            return races;
        }
        #endregion

        #region GetRaceById
        public async Task<Course> GetCourseByIdAsync(int id)
        {
            var course = await SqlAzureRetry.ExecuteAsync(async () => await Context.Courses
          .FirstOrDefaultAsync(c => c.CourseId == id));

            return course;
        }
        #endregion

        #region GetConcurrentsByRace
        public async Task<List<Concurrent>> GetConcurrentsByRaceAsync(int idCourse)
        {
            var courses = await SqlAzureRetry.ExecuteAsync(async () => await Context.Courses
                .Where(c => c.CourseId == idCourse)
                .SelectMany(c => c.Concurrents)
                .OrderBy(c => c.Nom)
                .ToListAsync());
            return courses;
        }
        #endregion

        #region Create
        public void Create(Course course)
        {
            Context.Courses.Add(course);
        }
        #endregion

        #region Like
        public async Task LikeAsync(int idCourse)
        {
            Course course = await SqlAzureRetry.ExecuteAsync(async () => await Context.Courses.FirstOrDefaultAsync(c => c.CourseId == idCourse));
            course.Likes++;
        }
        #endregion
    }
}
