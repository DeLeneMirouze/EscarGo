using EscarGo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System;

namespace EscarGo.Repositories
{
    public class CourseRepository : BaseDataRepository, ICourseRepository
    {
        #region Constructeur
        public CourseRepository(EscarGoContext context) : base(context)
        {

        } 
        #endregion

        #region GetCourses
        public List<Course> GetCourses()
        {
            var courses = Context.Courses
                .Where(c => c.Date >= DateTime.Now)
                .OrderByDescending(c => c.Date)
                .ThenBy(c => c.Pays)
                                .ThenBy(c => c.Label)
                .ToList();

            return courses;
        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            var courses = await Context.Courses
                .Where(c => c.Date >= DateTime.Now)
                .OrderByDescending(c => c.Date)
                .ThenBy(c => c.Pays)
                                .ThenBy(c => c.Label)
                .ToListAsync();

            return courses;
        }
        #endregion

        #region GetCourseById
        public Course GetCourseById(int id)
        {
            var course = Context.Courses
          .FirstOrDefault(c => c.IdCourse == id);

            return course;
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            var course = await Context.Courses
          .FirstOrDefaultAsync(c => c.IdCourse == id);

            return course;
        }
        #endregion

        #region GetConcurrentsByCourse
        public List<Concurrent> GetConcurrentsByCourse(int idCourse)
        {
            var courses = Context.Courses
                .Where(c => c.IdCourse == idCourse)
                .SelectMany(c => c.Concurrents)
                .OrderBy(c => c.Nom)
                .ToList();
            return courses;
        }

        public async Task<List<Concurrent>> GetConcurrentsByCourseAsync(int idCourse)
        {
            var courses = await Context.Courses
                .Where(c => c.IdCourse == idCourse)
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
            Course course = Context.Courses.FirstOrDefault(c => c.IdCourse == idCourse);
            course.Likes++;

            Context.SaveChanges();
        }

        public async Task LikeAsync(int idCourse)
        {
            Course course = await Context.Courses.FirstOrDefaultAsync(c => c.IdCourse == idCourse);
            course.Likes++;

            await Context.SaveChangesAsync();
        } 
        #endregion
    }
}
