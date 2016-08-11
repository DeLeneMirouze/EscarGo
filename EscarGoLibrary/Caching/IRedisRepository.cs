#region using
using EscarGoLibrary.Storage.Model;
using System.Collections.Generic;
#endregion

namespace EscarGoLibrary.Caching
{
    public interface IRedisRepository
    {
        List<RaceEntity> GetRaceDetail(int raceId);
        List<CompetitorEntity> GetCompetitorDetail(int competitorId);
    }
}