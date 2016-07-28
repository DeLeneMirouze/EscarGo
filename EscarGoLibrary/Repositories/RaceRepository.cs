﻿#region using

using EscarGoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
#endregion

namespace EscarGoLibrary.Repositories
{
    public class RaceRepository : BaseDataRepository, IRaceRepository
    {
        #region Constructeur
        public RaceRepository(EscarGoContext context) : base(context)
        {

        }
        #endregion

        #region GetRaces
        private IQueryable<Course> GetRequest(int recordsPerPage, int currentPage)
        {
            var query = Context.Courses
               .Where(c => c.Date >= DateTime.UtcNow)
               .OrderByDescending(c => c.Date)
               .ThenBy(c => c.Pays)
               .ThenBy(c => c.Label)
               .Skip(currentPage * recordsPerPage);

            if (recordsPerPage > 0)
            {
                query = query.Take(recordsPerPage);
            }

            return query;
        }

        public List<Course> GetRaces(int recordsPerPage, int currentPage)
        {
            var req = GetRequest(recordsPerPage, currentPage);
            List<Course> races = req.ToList();

            if (races.Count == 0 && currentPage > 0)
            {
                currentPage--;
                req = GetRequest(recordsPerPage, currentPage);
                races = req.ToList();
            }

            return races;
        }

        #endregion

        #region GetRaceById
        public Course GetRaceById(int id)
        {
            var course = Context.Courses
          .FirstOrDefault(c => c.CourseId == id);

            return course;
        }

        #endregion

        #region GetConcurrentsByRace
        public List<Concurrent> GetConcurrentsByRace(int idCourse)
        {
            var courses = Context.Courses
                .Where(c => c.CourseId == idCourse)
                .SelectMany(c => c.Concurrents)
                .OrderBy(c => c.Nom)
                .ToList();
            return courses;
        }

        #endregion

        #region Create
        public void Create(Course course)
        {
            Context.Courses.Add(course);
        }
        #endregion

        #region Like
        public void Like(int idCourse)
        {
            Course course = Context.Courses.FirstOrDefault(c => c.CourseId == idCourse);
            course.Likes++;
        }
 
        #endregion
    }
}
