using EscarGoLibrary.Models;

namespace EscarGoLibrary.Caching
{
    public interface IRedisRepository
    {
        Concurrent GetCompetitor(int competitorId);
        Course GetRace(int raceId);
    }
}