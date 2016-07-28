using EscarGoLibrary.Models;
using EscarGoLibrary.Storage.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories.CQRS
{
    public interface IRaceRepositoryCQRS
    {
        Task LikeAsync(int idCourse);
        void Create(Course course);
        List<RaceEntity> GetRaceDetail(int idRace);
        List<Course> GetRaces();
    }
}
