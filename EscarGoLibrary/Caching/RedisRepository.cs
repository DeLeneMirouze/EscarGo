#region using
using EscarGoLibrary.Storage.Model;
using EscarGoLibrary.Storage.Repository;
using System;
using System.Collections.Generic;
#endregion

namespace EscarGoLibrary.Caching
{
    public class RedisRepository : IRedisRepository
    {
        #region Constructeur
        readonly ITableStorageRepository _tableStorageRepository;
        public RedisRepository(ITableStorageRepository tableStorageRepository)
        {
            _tableStorageRepository = tableStorageRepository;
        }
        #endregion

        #region GetRaceDetail
        public List<RaceEntity> GetRaceDetail(int raceId)
        {
            string key = RedisCache.CreateRaceKey(raceId);

            Func<List<RaceEntity>> loadingFunction = () =>
            {
                List<RaceEntity> races = _tableStorageRepository.GetRaceById(raceId);

                return races;
            };
            TimeSpan sliding = new TimeSpan(0, 2, 10);
            List<RaceEntity> entities = RedisCache.Get(key, loadingFunction, sliding);

            return entities;
        }
        #endregion

        #region GetCompetitorDetail
        public List<CompetitorEntity> GetCompetitorDetail(int competitorId)
        {
            string key = RedisCache.CreateCompetitorKey(competitorId);

            Func<List<CompetitorEntity>> loadingFunction = () =>
            {
                List<CompetitorEntity> competitors = _tableStorageRepository.GetCompetitorById(competitorId);

                return competitors;
            };
            TimeSpan sliding = new TimeSpan(0, 2, 10);
            List<CompetitorEntity> entities = RedisCache.Get<List<CompetitorEntity>>(key, loadingFunction, sliding);

            return entities;
        } 
        #endregion
    }
}
