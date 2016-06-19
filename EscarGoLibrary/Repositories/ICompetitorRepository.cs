using EscarGoLibrary.Models;
using System;
using System.Collections.Generic;

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
    }
}
