using EscarGo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscarGo.Repositories
{
    public interface ICourseRepository
    {
        List<Course> GetCourses();
        Course GetCourseById(int id);
        List<Concurrent> GetConcurrentsByCourse(int idCourse);

        Task<List<Course>> GetCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task<List<Concurrent>> GetConcurrentsByCourseAsync(int idCourse);
    }
}
