
using EscarGoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories
{
    public class CourseRepository : BaseDataRepository, ICourseRepository, ICourseRepositoryAsync
    {
        #region Constructeur
        public CourseRepository(EscarGoContext context) : base(context)
        {

        }
        #endregion

        #region GetCourses
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

        public List<Course> GetCourses(int recordsPerPage, int currentPage)
        {
            var req = GetRequest(recordsPerPage, currentPage);
            List<Course> courses = req.ToList();

            if (courses.Count == 0 && currentPage > 0)
            {
                currentPage--;
                req = GetRequest(recordsPerPage, currentPage);
                courses = req.ToList();
            }

            return courses;
        }

        public async Task<List<Course>> GetCoursesAsync(int recordsPerPage, int currentPage)
        {
            var req = GetRequest(recordsPerPage, currentPage);
            List<Course> courses = await req.ToListAsync();

            if (courses.Count == 0 && currentPage > 0)
            {
                currentPage--;
                req = GetRequest(recordsPerPage, currentPage);
                courses = await req.ToListAsync();
            }

            return courses;
        }
        #endregion

        #region GetCourseById
        public Course GetCourseById(int id)
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

        #region GetConcurrentsByCourse
        public List<Concurrent> GetConcurrentsByCourse(int idCourse)
        {
            var courses = Context.Courses
                .Where(c => c.CourseId == idCourse)
                .SelectMany(c => c.Concurrents)
                .OrderBy(c => c.Nom)
                .ToList();
            return courses;
        }

        public async Task<List<Concurrent>> GetConcurrentsByCourseAsync(int idCourse)
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
