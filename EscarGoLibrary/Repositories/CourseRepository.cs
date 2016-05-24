using EscarGo.Models;
using System.Collections.Generic;
using System.Linq;

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
        #endregion

        #region GetCourseById
        public Course GetCourseById(int id)
        {
            var course = Context.Courses
          .FirstOrDefault(c => c.IdCourse == id);

            return course;
        }
        #endregion
    }
}
