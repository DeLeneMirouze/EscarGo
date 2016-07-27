using EscarGoLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories.CQRS
{
    public interface IRaceRepositoryCQRS
    {
        Task LikeAsync(int idCourse);
        void Create(Course course);
        Task<List<Concurrent>> GetConcurrentsByRaceAsync(int idCourse);
        Task<Course> GetCourseByIdAsync(int id);
        List<Course> GetRaces();
    }
}
