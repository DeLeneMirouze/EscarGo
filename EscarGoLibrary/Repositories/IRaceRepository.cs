using EscarGoLibrary.Models;
using System.Collections.Generic;

namespace EscarGoLibrary.Repositories
{
    public interface IRaceRepository
    {
        List<Course> GetRaces(int recordsPerPage, int currentPage);
        Course GetRaceById(int id);
        List<Concurrent> GetConcurrentsByRace(int idCourse);
        void Create(Course course);
        void Like(int idCourse);
    }
}
