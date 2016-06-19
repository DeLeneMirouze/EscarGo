using EscarGoLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories
{
    public interface ICourseRepositoryAsync
    {
        Task<List<Course>> GetCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task<List<Concurrent>> GetConcurrentsByCourseAsync(int idCourse);
        Task CreateAsync(Course course);
        Task LikeAsync(int idCourse);
    }
}
