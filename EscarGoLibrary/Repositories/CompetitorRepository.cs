using EscarGoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories
{
    public class CompetitorRepository : BaseDataRepository, ICompetitorRepository, ICompetitorRepositoryAsync
    {
        public CompetitorRepository(EscarGoContext context) : base(context)
        {

        }

        #region GetCompetitors
        public List<Concurrent> GetCompetitors()
        {
            var concurrents = Context.Concurrents
                .Include("Entraineur")
          .OrderBy(c => c.Nom)
          .ToList();

            return concurrents;
        }

        public async Task<List<Concurrent>> GetCompetitorsAsync()
        {
            var concurrents = await Context.Concurrents
                .Include("Entraineur")
          .OrderBy(c => c.Nom)
          .ToListAsync();

            return concurrents;
        }
        #endregion

        #region GetCompetitorById
        public Concurrent GetCompetitorById(int id)
        {
            var concurrent = Context.Concurrents
                    .Include("Entraineur")
          .FirstOrDefault(c => c.ConcurrentId == id);

            return concurrent;
        }

        public async Task<Concurrent> GetCompetitorByIdAsync(int id)
        {
            var concurrent = await Context.Concurrents
                    .Include("Entraineur")
                            .FirstOrDefaultAsync(c => c.ConcurrentId == id);

            return concurrent;
        }
        #endregion

        #region GetRacesByCompetitor
        public List<Course> GetRacesByCompetitor(int id)
        {
            var paris = Context.Paris
                .Include("Course")
                .Where(p => p.ConcurrentId == id).ToList();
            var courses = paris.OrderBy(p => p.Course.Date).Select(p => p.Course).ToList();
            return courses;
        }

        public async Task<List<Course>> GetRacesByCompetitorAsync(int id)
        {
            List<Pari> paris = await Context.Paris
                .Include("Course")
                .Where(p => p.ConcurrentId == id).ToListAsync();
            var courses = paris.OrderBy(p => p.Course.Date).Select(p => p.Course).ToList();

            return courses;
        }
        #endregion

        #region GetBetsByCompetitor
        public List<Pari> GetBetsByCompetitor(int id)
        {
            var paris = Context.Paris
                .Include("Course")
                .Where(p => p.ConcurrentId == id).ToList();
            return paris;
        }

        public async Task<List<Pari>> GetBetsByCompetitorAsync(int id)
        {
            var paris = await Context.Paris
                .Include("Course")
                .Where(p => p.ConcurrentId == id).ToListAsync();
            return paris;
        }
        #endregion

        #region GetBetsByRace
        public List<Pari> GetBetsByRace(int idCourse)
        {
            var paris = Context.Paris
                .Include("Course")
                .Where(p => p.CourseId == idCourse).ToList();
            return paris;
        }

        public async Task<List<Pari>> GetBetsByRaceAsync(int idCourse)
        {
            var paris = await Context.Paris
                .Include("Course")
                .Where(p => p.CourseId == idCourse).ToListAsync();
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

            Context.SaveChanges();
        }

        public async Task SetBetAsync(int idCourse, int concurrentId)
        {
            // pari sur lequel on parie
            var pari = await Context.Paris
                .Where(p => p.CourseId == idCourse && p.ConcurrentId == concurrentId)
                .FirstOrDefaultAsync();
            if (pari == null)
            {
                return;
            }
            pari.NbParis++; // enregistre le pari

            // les paris de la course
            var paris = await Context.Paris.Where(c => c.CourseId == idCourse).ToListAsync();
            // somme de tous les paris de la course
            int total = paris.Sum(c => c.NbParis);
            // recalcul de la cote pour chaque pari de la course
            paris.ForEach(p =>
            {
                p.SC = (double)total / p.NbParis;
                p.SC = Math.Round(10 * p.SC) / 10;
            });

            await Context.SaveChangesAsync();
        }
        #endregion
    }
}
