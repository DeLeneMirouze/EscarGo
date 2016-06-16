using EscarGoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories
{
    public interface ICompetitorRepository: IDisposable
    {
        List<Concurrent> GetCompetitors();
        Concurrent GetCompetitorById(int id);
        List<Course> GetRacesByCompetitor(int id);
        List<Pari> GetBetsByCompetitor(int id);
        void SetBet(int idCourse, int idConcurrent);
        List<Pari> GetBetsByRace(int idCourse);

        Task<List<Pari>> GetBetsByRaceAsync(int idCourse);
        Task<List<Concurrent>> GetCompetitorsAsync();
        Task SetBetAsync(int idCourse, int idConcurrent);
       Task<List<Pari>> GetBetsByCompetitorAsync(int id);
       Task< List<Course>> GetRacesByCompetitorAsync(int id);
        Task<Concurrent> GetCompetitorByIdAsync(int id);
    }
}
