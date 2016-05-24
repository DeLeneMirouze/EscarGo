using EscarGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EscarGo.Repositories
{
    public class ConcurrentRepository : BaseDataRepository, IConcurrentRepository
    {
        public ConcurrentRepository(EscarGoContext context) : base(context)
        {

        }

        #region GetConcurrents
        public List<Concurrent> GetConcurrents()
        {
            var concurrents = Context.Concurrents
          .OrderBy(c => c.Nom)
          .ToList();

            return concurrents;
        }
        #endregion

        #region GetConcurrentById
        public Concurrent GetConcurrentById(int id)
        {
            var concurrent = Context.Concurrents
          .FirstOrDefault(c => c.IdConcurrent == id);

            return concurrent;
        }
        #endregion

        #region GetCoursesByConcurrent
        public List<Course> GetCoursesByConcurrent(int id)
        {
            var paris = Context.Paris
                .Include("Course")
                .Where(p => p.IdConcurrent == id).ToList();
            var courses = paris.OrderBy(p => p.Course.Date).Select(p => p.Course).ToList();
            return courses;
        }
        #endregion

        #region GetParisByConcurrent
        public List<Pari> GetParisByConcurrent(int id)
        {
            var paris = Context.Paris
                .Include("Course")
                .Where(p => p.IdConcurrent == id).ToList();
            return paris;
        }
        #endregion

        #region SetBet
        public void SetBet(int idCourse, int idConcurrent)
        {
            // pari sur lequel on parie
            var pari = Context.Paris
                .Where(p => p.IdCourse == idCourse && p.IdConcurrent == idConcurrent)
                .FirstOrDefault();
            if (pari == null)
            {
                return;
            }
            pari.NbParis++; // enregistre le pari

            // les paris de la course
            var paris = Context.Paris.Where(c => c.IdCourse == idCourse).ToList();
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
        #endregion
    }
}
