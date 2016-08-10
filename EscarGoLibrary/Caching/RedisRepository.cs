#region using
using EscarGoLibrary.Models;
using EscarGoLibrary.Storage.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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

        #region GetRace
        public Course GetRace(int raceId)
        {
            string key = RedisCache.CreateRaceKey(raceId);

            Func<Course> loadingFunction = () =>
            {
               List<Course> courses = _tableStorageRepository.GetRaces();

                return courses.Where(c => c.CourseId == raceId).FirstOrDefault();
            };
            Course course = RedisCache.Get<Course>(key, loadingFunction);

            return course;
        }
        #endregion

        #region GetCompetitor
        public Concurrent GetCompetitor(int competitorId)
        {
            string key = RedisCache.CreateRaceKey(competitorId);

            Func<Concurrent> loadingFunction = () =>
            {
                List<Concurrent> competitors = _tableStorageRepository.GetCompetitors();

                return competitors.Where(c => c.ConcurrentId == competitorId).FirstOrDefault();
            };
            Concurrent course = RedisCache.Get<Concurrent>(key, loadingFunction);

            return course;
        } 
        #endregion
    }
}
