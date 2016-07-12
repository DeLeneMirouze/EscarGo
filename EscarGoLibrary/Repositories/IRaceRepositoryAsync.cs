using EscarGoLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories
{
    public interface IRaceRepositoryAsync
    {
        Task<List<Course>> GetRacesAsync(int recordsPerPage, int currentPage);
        Task<Course> GetCourseByIdAsync(int id);
        Task<List<Concurrent>> GetConcurrentsByRaceAsync(int idCourse);
        void Create(Course course);
        Task LikeAsync(int idCourse);
    }
}
