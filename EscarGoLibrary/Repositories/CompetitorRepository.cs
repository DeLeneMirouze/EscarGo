using EscarGoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace EscarGoLibrary.Repositories
{
    public class CompetitorRepository : BaseDataRepository, ICompetitorRepository
    {
        #region Constructeur
        public CompetitorRepository(EscarGoContext context) : base(context)
        {

        } 
        #endregion

        #region GetCompetitors
        public List<Concurrent> GetCompetitors()
        {
            var concurrents = SqlAzureRetry.ExecuteAction(() => Context.Concurrents
                .Include("Entraineur")
          .OrderBy(c => c.Nom)
          .ToList());

            return concurrents;
        }

        #endregion

        #region GetCompetitorById
        public Concurrent GetCompetitorById(int id)
        {
            var concurrent = SqlAzureRetry.ExecuteAction(() => Context.Concurrents
                    .Include("Entraineur")
          .FirstOrDefault(c => c.ConcurrentId == id));

            return concurrent;
        }

        #endregion

        #region GetRacesByCompetitor
        public List<Course> GetRacesByCompetitor(int id)
        {
            var paris = SqlAzureRetry.ExecuteAction(() => Context.Paris
                .Include("Course")
                .Where(p => p.ConcurrentId == id).ToList());

            var courses = paris.OrderBy(p => p.Course.Date).Select(p => p.Course).ToList();
            return courses;
        }

        #endregion

        #region GetBetsByCompetitor
        public List<Pari> GetBetsByCompetitor(int id)
        {
            var paris = SqlAzureRetry.ExecuteAction(() => Context.Paris
                .Include("Course")
                .Where(p => p.ConcurrentId == id).ToList());
            return paris;
        }

        #endregion

        #region GetBetsByRace
        public List<Pari> GetBetsByRace(int idCourse)
        {
            var paris = SqlAzureRetry.ExecuteAction(() => Context.Paris
                .Include("Course")
                .Where(p => p.CourseId == idCourse).ToList());
            return paris;
        }

        #endregion

        #region SetBet
        public void SetBet(int idCourse, int concurrentId)
        {
            // pari sur lequel on parie
            var pari = Context.Paris
                .Where(p => p.CourseId == idCourse && p.ConcurrentId == concurrentId)
                .FirstOrDefault();
            if (pari == null)
            {
                return;
            }
            pari.NbParis++; // enregistre le pari

            // les paris de la course
            var paris = Context.Paris.Where(c => c.CourseId == idCourse).ToList();
            // somme de tous les paris de la course
            int total = paris.Sum(c => c.NbParis);
            // recalcul de la cote pour chaque pari de la course
            paris.ForEach(p =>
            {
                p.SC = (double)total / p.NbParis;
                p.SC = Math.Round(10 * p.SC) / 10;
            });
        }

        #endregion
    }
}
