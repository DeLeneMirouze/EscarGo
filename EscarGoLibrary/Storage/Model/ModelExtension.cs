using EscarGoLibrary.Models;
using System;

namespace EscarGoLibrary.Storage.Model
{
    public static class ModelExtension
    {
        #region Concurrent
        public static Concurrent ToConcurrent(this CompetitorEntity entity)
        {
            Concurrent concurrent = new Concurrent();
            concurrent.Nom = entity.Nom;
            concurrent.ConcurrentId = Convert.ToInt32(entity.PartitionKey);
            concurrent.Defaites = entity.Defaites;
            concurrent.Entraineur = new Entraineur() { Nom = entity.Entraineur };
            concurrent.SC = entity.SC;
            concurrent.Victoires = entity.Victoires;

            return concurrent;
        }

        public static Concurrent ToConcurrent(this RaceEntity entity)
        {
            Concurrent concurrent = new Concurrent();
            concurrent.Nom = entity.Concurrent;
            concurrent.ConcurrentId = Convert.ToInt32(entity.PartitionKey);
            concurrent.SC = entity.SC;

            return concurrent;
        }

        public static CompetitorEntity ToCompetitorEntity(this Concurrent concurrent)
        {
            CompetitorEntity competitorEntity = new CompetitorEntity();
            competitorEntity.Victoires = concurrent.Victoires;
            competitorEntity.SC = concurrent.SC;
            competitorEntity.Nom = concurrent.Nom;
            competitorEntity.Entraineur = concurrent.Entraineur.Nom;
            competitorEntity.Defaites = concurrent.Defaites;

            return competitorEntity;
        } 
        #endregion

        #region Course

        public static RaceEntity ToRaceEntity(this Course course)
        {
            RaceEntity raceEntity = new RaceEntity();
            raceEntity.Date = course.Date;
            raceEntity.Label = course.Label;
            raceEntity.Pays = course.Pays;
            raceEntity.Likes = course.Likes;
            raceEntity.SC = course.SC;
            raceEntity.Ville = course.Ville;
            raceEntity.NbTickets = course.NbTickets;

            return raceEntity;
        }

        public static Course ToCourse(this RaceEntity entity)
        {
            Course course = new Course();
            course.Date = entity.Date;
            course.CourseId = Convert.ToInt32(entity.PartitionKey);
            course.Label = entity.Label;
            course.Likes = entity.Likes;
            course.Pays = entity.Pays;
            course.SC = entity.SC;
            course.Ville = entity.Ville;
            course.NbTickets = entity.NbTickets;

            return course;
        }

        public static Course ToCourse(this CompetitorEntity entity)
        {
            Course course = new Course();
            course.Date = entity.Date;
            course.CourseId = Convert.ToInt32(entity.RowKey);
            course.SC = entity.SC;
            course.Label = entity.Course;

            return course;
        } 
        #endregion
    }
}
