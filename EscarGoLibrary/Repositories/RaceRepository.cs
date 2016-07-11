
using EscarGoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories
{
    public class RaceRepository : BaseDataRepository, IRaceRepository, IRaceRepositoryAsync
    {
        #region Constructeur
        public RaceRepository(EscarGoContext context) : base(context)
        {

        }
        #endregion

        #region GetRaces
        private IQueryable<Course> GetRequest(int recordsPerPage, int currentPage)
        {
            return Context.Courses
               .Where(c => c.Date >= DateTime.Now)
               .OrderByDescending(c => c.Date)
               .ThenBy(c => c.Pays)
               .ThenBy(c => c.Label)
               .Skip(currentPage * recordsPerPage)
               .Take(recordsPerPage);
        }

        public List<Course> GetRaces(int recordsPerPage, int currentPage)
        {
            var req = GetRequest(recordsPerPage, currentPage);
            List<Course> races = req.ToList();

            if (races.Count == 0 && currentPage > 0)
            {
                currentPage--;
                req = GetRequest(recordsPerPage, currentPage);
                races = req.ToList();
            }

            return races;
        }

        public async Task<List<Course>> GetRacesAsync(int recordsPerPage, int currentPage)
        {
            var req = GetRequest(recordsPerPage, currentPage);
            List<Course> races = await req.ToListAsync();

            if (races.Count == 0 && currentPage > 0)
            {
                currentPage--;
                req = GetRequest(recordsPerPage, currentPage);
                races = await req.ToListAsync();
            }

            return races;
        }
        #endregion

        #region GetRaceById
        public Course GetRaceById(int id)
        {
            var course = Context.Courses
          .FirstOrDefault(c => c.CourseId == id);

            return course;
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            var course = await Context.Courses
          .FirstOrDefaultAsync(c => c.CourseId == id);

            return course;
        }
        #endregion

        #region GetConcurrentsByRace
        public List<Concurrent> GetConcurrentsByRace(int idCourse)
        {
            var courses = Context.Courses
                .Where(c => c.CourseId == idCourse)
                .SelectMany(c => c.Concurrents)
                .OrderBy(c => c.Nom)
                .ToList();
            return courses;
        }

        public async Task<List<Concurrent>> GetConcurrentsByRaceAsync(int idCourse)
        {
            var courses = await Context.Courses
                .Where(c => c.CourseId == idCourse)
                .SelectMany(c => c.Concurrents)
                .OrderBy(c => c.Nom)
                .ToListAsync();
            return courses;
        }
        #endregion

        #region Create
        public void Create(Course course)
        {
            Context.Courses.Add(course);
            Context.SaveChanges();
        }

        public async Task CreateAsync(Course course)
        {
            Context.Courses.Add(course);
            await Context.SaveChangesAsync();
        }
        #endregion

        #region Like
        public void Like(int idCourse)
        {
            Course course = Context.Courses.FirstOrDefault(c => c.CourseId == idCourse);
            course.Likes++;

            Context.SaveChanges();
        }

        public async Task LikeAsync(int idCourse)
        {
            Course course = await Context.Courses.FirstOrDefaultAsync(c => c.CourseId == idCourse);
            course.Likes++;

            await Context.SaveChangesAsync();
        } 
        #endregion
    }
}
