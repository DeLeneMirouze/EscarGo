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
        void Create(Course course);

        Task<List<Course>> GetCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task<List<Concurrent>> GetConcurrentsByCourseAsync(int idCourse);
        Task CreateAsync(Course course);
    }
}
