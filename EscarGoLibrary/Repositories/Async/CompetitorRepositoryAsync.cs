﻿using EscarGoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EscarGoLibrary.Repositories.Async
{
    public class CompetitorRepositoryAsync : BaseDataRepository, ICompetitorRepositoryAsync
    {
        #region Constructeur
        public CompetitorRepositoryAsync(EscarGoContext context) : base(context)
        {

        }
        #endregion

        #region GetCompetitors
        public async Task<List<Concurrent>> GetCompetitorsAsync()
        {
            var concurrents = await SqlAzureRetry.ExecuteAsync(async () => await Context.Concurrents
                .Include("Entraineur")
          .OrderBy(c => c.Nom)
          .ToListAsync());

            return concurrents;
        }
        #endregion

        #region GetCompetitorById
        public async Task<Concurrent> GetCompetitorByIdAsync(int id)
        {
            var concurrent = await SqlAzureRetry.ExecuteAsync(async () => await Context.Concurrents
                    .Include("Entraineur")
                            .FirstOrDefaultAsync(c => c.ConcurrentId == id));

            return concurrent;
        }
        #endregion

        #region GetRacesByCompetitor
        public async Task<List<Course>> GetRacesByCompetitorAsync(int id)
        {
            List<Pari> paris = await SqlAzureRetry.ExecuteAsync(async () => await Context.Paris
                .Include("Course")
                .Where(p => p.ConcurrentId == id).ToListAsync());
            var courses = paris.OrderBy(p => p.Course.Date).Select(p => p.Course).ToList();

            return courses;
        }
        #endregion

        #region GetBetsByCompetitor
        public async Task<List<Pari>> GetBetsByCompetitorAsync(int id)
        {
            var paris = await SqlAzureRetry.ExecuteAsync(async () => await Context.Paris
                .Include("Course")
                .Where(p => p.ConcurrentId == id).ToListAsync());
            return paris;
        }
        #endregion

        #region GetBetsByRace
        public async Task<List<Pari>> GetBetsByRaceAsync(int idCourse)
        {
            var paris = await SqlAzureRetry.ExecuteAsync(async () => await Context.Paris
                .Include("Course")
                .Where(p => p.CourseId == idCourse).ToListAsync());
            return paris;
        }
        #endregion

        #region SetBet
        public async Task SetBetAsync(int idCourse, int concurrentId)
        {
            // pari sur lequel on parie
            var pari = await SqlAzureRetry.ExecuteAsync(async () => await Context.Paris
                .Where(p => p.CourseId == idCourse && p.ConcurrentId == concurrentId)
                .FirstOrDefaultAsync());

            if (pari == null)
            {
                return;
            }
            pari.NbParis++; // enregistre le pari

            // les paris de la course
            var paris = await SqlAzureRetry.ExecuteAsync(async () => await Context.Paris.Where(c => c.CourseId == idCourse).ToListAsync());
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
