using EscarGo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EscarGo.Repositories
{
    public class CourseRepository : BaseDataRepository, ICourseRepository
    {
        public CourseRepository(EscarGoContext context):base(context)
        {

        }

        #region GetCourses
        public List<Course> GetCourses()
        {
            var courses = Context.Courses
                .OrderByDescending(c => c.Date)
                .ThenBy(c => c.Pays)
                                .ThenBy(c => c.Label)
                .ToList();

            return courses;
        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            var courses = await Context.Courses
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
    }
}
