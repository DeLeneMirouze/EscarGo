#region using
using EscarGoLibrary.Models;
using EscarGoLibrary.Repositories;
using EscarGoLibrary.Storage.Model;
using EscarGoLibrary.Storage.Repository;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace CQRSJobs
{
    public sealed class Functions
    {
        #region Constructeur
        static EscarGoContext context;
        static ICompetitorRepositoryAsync _competitorRepository;
        static ITableStorageRepository _storage;
        static IRaceRepositoryAsync _raceRepository;

        static Functions()
        {
            context = new EscarGoContext();
            _competitorRepository = new CompetitorRepository(context);
            _storage = new TableStorageRepository();
            _raceRepository = new RaceRepository(context);
        } 
        #endregion

        #region ProcessCompetitors
        [NoAutomaticTrigger]
        public static async Task ProcessCompetitors(TextWriter log)
        {
            try
            {
                List<Concurrent> concurrents = await _competitorRepository.GetCompetitorsAsync();

                List<CompetitorEntity> entities = new List<CompetitorEntity>();
                foreach (Concurrent concurrent in concurrents)
                {
                    var hisRaces = await _competitorRepository.GetRacesByCompetitorAsync(concurrent.ConcurrentId);
                    var paris = await _competitorRepository.GetBetsByCompetitorAsync(concurrent.ConcurrentId);

                    foreach (Course race in hisRaces)
                    {
                        CompetitorEntity entity = concurrent.ToCompetitorEntity();
                        entity.PartitionKey = concurrent.ConcurrentId.ToString();

                        entity.Course = race.Label;
                        entity.Date = race.Date;
                        entity.RowKey = race.CourseId.ToString();
                        entity.SC = paris.FirstOrDefault(p => p.CourseId == race.CourseId).SC;

                        entities.Add(entity);
                    }
                }

                _storage.SetCompetitors(entities);

                await log.WriteLineAsync("Mise à jour du lot réussie");
            }
            catch (Exception ex)
            {
                await log.WriteLineAsync(ex.Message);
            }
        }
        #endregion

        #region ProcessRaces
        [NoAutomaticTrigger]
        public static async Task ProcessRaces(TextWriter log)
        {
            try
            {
                List<Course> courses = await _raceRepository.GetRacesAsync(0, 0);

                List<RaceEntity> entities = new List<RaceEntity>();
                foreach (Course course in courses)
                {
                    var hisCompetitors = await _raceRepository.GetConcurrentsByRaceAsync(course.CourseId);
                    var paris = await _competitorRepository.GetBetsByRaceAsync(course.CourseId);

                    foreach (Concurrent competitor in hisCompetitors)
                    {
                        RaceEntity entity = course.ToRaceEntity();
                        entity.PartitionKey = course.CourseId.ToString();

                        entity.Concurrent = competitor.Nom;
                        entity.RowKey = competitor.ConcurrentId.ToString();
                        entity.SC = paris.FirstOrDefault(p => p.ConcurrentId == competitor.ConcurrentId).SC;

                        entities.Add(entity);
                    }
                }

                _storage.SetRaces(entities);

                await log.WriteLineAsync("Mise à jour du lot réussie");
            }
            catch (Exception ex)
            {
                await log.WriteLineAsync(ex.Message);
            }
        }
        #endregion
    }
}
