using EscarGoLibrary.Models;
using EscarGoLibrary.Storage.Model;
using System.Collections.Generic;

namespace EscarGoLibrary.Storage.Repository
{
    public interface ITableStorageRepository
    {
        List<Concurrent> GetCompetitors(string competitorId);
        List<Course> GetRaces();
        void SetCompetitors(List<CompetitorEntity> competitors);
        void SetRaces(List<RaceEntity> races);
    }
}
