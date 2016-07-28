using EscarGoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories.Async
{
    public interface ICompetitorRepositoryAsync : IDisposable
    {
        Task<List<Pari>> GetBetsByRaceAsync(int idCourse);
        Task<List<Concurrent>> GetCompetitorsAsync();
        Task SetBetAsync(int idCourse, int idConcurrent);
        Task<List<Pari>> GetBetsByCompetitorAsync(int id);
        Task<List<Course>> GetRacesByCompetitorAsync(int id);
        Task<Concurrent> GetCompetitorByIdAsync(int id);
    }
}
