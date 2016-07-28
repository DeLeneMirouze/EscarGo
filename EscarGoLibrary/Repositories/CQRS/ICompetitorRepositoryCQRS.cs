using EscarGoLibrary.Models;
using EscarGoLibrary.Storage.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories.CQRS
{
    public interface ICompetitorRepositoryCQRS
    {
        List<Concurrent> GetCompetitors();
        List<CompetitorEntity> GetCompetitorDetail(int id);
        Task SetBetAsync(int idCourse, int concurrentId);
        Task<List<Pari>> GetBetsByRace(int idCourse);
    }
}
