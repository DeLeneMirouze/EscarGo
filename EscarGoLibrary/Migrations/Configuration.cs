namespace EscarGoLibrary.Migrations
{
    using Models;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EscarGoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EscarGoContext context)
        {
            //  This method will be called after migrating to the latest version.
            BuildData();

            context.Courses.AddOrUpdate(p => p.CourseId, listeCourses.ToArray());
            context.Concurrents.AddOrUpdate(p => p.ConcurrentId, concurrents);
            context.Paris.AddOrUpdate(p => p.PariId, paris.ToArray());
            context.Entraineurs.AddOrUpdate(p => p.EntraineurId, entraineurs.ToArray());
            context.Visiteurs.AddOrUpdate(p => p.Id, visiteurs.ToArray());
        }

        #region BuildData 
        Concurrent[] concurrents;
        List<Course> listeCourses;
        List<Pari> paris;
        List<Entraineur> entraineurs;
        List<Visiteur> visiteurs;

        public void BuildData()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);

            // liste des entraineurs
            entraineurs = new List<Entraineur>();
            Entraineur entraineur = new Entraineur() { EntraineurId = 1, Nom = "Dr YaCommeUneMagouille" };
            entraineurs.Add(entraineur);
            entraineur = new Entraineur() { EntraineurId = 2, Nom = "Papy Emile" };
            entraineurs.Add(entraineur);
            entraineur = new Entraineur() { EntraineurId = 3, Nom = "Moumoune" };
            entraineurs.Add(entraineur);
            entraineur = new Entraineur() { EntraineurId = 4, Nom = "Paprika" };
            entraineurs.Add(entraineur);
            entraineur = new Entraineur() { EntraineurId =5, Nom = "La Carotte" };
            entraineurs.Add(entraineur);
            entraineur = new Entraineur() { EntraineurId = 6, Nom = "Patchouli" };
            entraineurs.Add(entraineur);
  

            // liste des valeureux candidats
            concurrents = new Concurrent[]
            {
                       new Concurrent { Nom = "Speedy Jet Trophy", Victoires = 100, Defaites = 0, IdEntraineur = 1,ConcurrentId=1 },
                   new Concurrent { Nom = "Spidi Gonzales", Victoires = 0, Defaites = 100, IdEntraineur = 2,ConcurrentId=2 },
                      new Concurrent { Nom = "La Foudre du Nord", Victoires = 2, Defaites = 8, IdEntraineur = 3,ConcurrentId=3 },
                           new Concurrent { Nom = "Cool Man", Victoires = 50, Defaites = 2, IdEntraineur = 4 ,ConcurrentId=4},
                                new Concurrent { Nom = "Salade", Victoires = 5, Defaites = 6, IdEntraineur = 5,ConcurrentId=5 },
                                     new Concurrent { Nom = "Super Gascon", Victoires = 3, Defaites = 3, IdEntraineur = 6 ,ConcurrentId=6},
        new Concurrent { Nom = "Gros Baveux", Victoires = 45, Defaites = 5, IdEntraineur = 4,ConcurrentId=7 },
             new Concurrent { Nom = "Petit Baveux", Victoires = 160, Defaites = 55, IdEntraineur = 5 ,ConcurrentId=8},
                  new Concurrent { Nom = "Persillade", Victoires = 2, Defaites = 5, IdEntraineur = 4,ConcurrentId=9 },
                       new Concurrent { Nom = "Doudou", Victoires = 2, Defaites = 3, IdEntraineur = 4 ,ConcurrentId=10},
                            new Concurrent { Nom = "Pilou Pilou", Victoires = 3, Defaites = 8, IdEntraineur = 6 ,ConcurrentId=11}
            };

            // liste des courses sur 2 ans
            int[] years = Enumerable.Range(DateTime.Now.Year, 2).ToArray();

            List<Course> courses = new List<Course>() {   new Course { Label = "Speedy Jet Trophy", Pays="Des Merveilles",Ville="Xanadu" },
                 new Course { Label = "Restau de la Gare", Pays = "France", Ville = "Bapaume (sud)" },
                 new Course { Label = "Cassoulet Lillois", Pays = "France", Ville = "Wazemmes" },
                        new Course { Label = "La Grande Course du Large", Pays = "Suisse", Ville = "Berne" },
                               new Course { Label = "Cache-cache", Pays = "Belgique", Ville = "Bruxelles" },
                                      new Course { Label = "Attrap'moi", Pays = "Tunisie", Ville = "Tunis" },
                                             new Course { Label = "Rally sans Frontière", Pays = "France", Ville="Paris" },
                                              new Course { Label = "La Grande Course", Pays = "Suisse",Ville="Bernes" },
                                               new Course { Label = "Radis et Salade", Pays = "Portugal", Ville = "Porto" }};

            // affectation des courses sur les 2 années et des concurrents pour chaque course
            int idCourse = 1;
            int[] myValues = Enumerable.Range(0, concurrents.Length).ToArray();
            listeCourses = new List<Course>();
            foreach (int year in years)
            {
                foreach (Course course in courses)
                {
                    if (rnd.NextDouble() < 0.1)
                    {
                        // la course n'a pas eu lieu
                        continue;
                    }

                    Course currentCourse = new Course();
                    currentCourse.Concurrents = new List<Concurrent>();
                    currentCourse.Label = course.Label;
                    currentCourse.Pays = course.Pays;
                    currentCourse.Ville = course.Ville;
                    currentCourse.CourseId = idCourse;
                    currentCourse.Likes = rnd.Next(500);
                    currentCourse.NbTickets = rnd.Next(500, 2000);
                    idCourse++;

                    // quand a lieu la course cette année là?
                    currentCourse.Date = new DateTime(year, 1 + rnd.Next(12), 1 + rnd.Next(28));

                    // qui participait? Il en faut au moins 2
                    int nbParticipants = 2 + rnd.Next(concurrents.Length - 1);
                    int[] threeRandom = myValues.OrderBy(x => rnd.Next()).Take(nbParticipants).ToArray();
                    for (int i = 0; i < threeRandom.Length; i++)
                    {
                        int valeur = threeRandom[i];
                        currentCourse.Concurrents.Add(concurrents[valeur]);
                        concurrents[valeur].Courses.Add(currentCourse);
                    }

                    listeCourses.Add(currentCourse);
                }

                // initialisation des paris
                paris = new List<Pari>();
                foreach (Course course in listeCourses)
                {
                    foreach (Concurrent concurrent in course.Concurrents)
                    {
                        Pari pari = new Pari();
                        pari.Concurrent = concurrent;
                        pari.ConcurrentId = concurrent.ConcurrentId;
                        pari.CourseId = course.CourseId;
                        pari.Course = course;
                        pari.DateDernierPari = DateTime.Now;
                        pari.NbParis = 1 + rnd.Next(10);

                        paris.Add(pari);
                    }
                }

                // calcul de la cote
                foreach (Course course in listeCourses)
                {
                    var pariCourses = paris.Where(p => p.CourseId == course.CourseId).ToList();
                    int total = pariCourses.Sum(p => p.NbParis);
                    pariCourses.ForEach(p =>
                    {
                        p.SC = (double)total / p.NbParis;
                        p.SC = Math.Round(10 * p.SC) / 10;
                    });
                }
            }


            visiteurs = new List<Visiteur>();
            Visiteur visiteur = new Visiteur() { Id = 1, Nom = "Batman" };
            visiteurs.Add(visiteur);
            visiteur = new Visiteur() { Id = 2, Nom = "Papy Emile" };
            visiteurs.Add(visiteur);
            visiteur = new Visiteur() { Id = 3, Nom = "Les compagnons de la salade" };
            visiteurs.Add(visiteur);
            visiteur = new Visiteur() { Id = 4, Nom = "Gaston Lagaffe" };
            visiteurs.Add(visiteur);
            visiteur = new Visiteur() { Id = 5, Nom = "Dr YaCommeUneMagouille" };
            visiteurs.Add(visiteur);
            visiteur = new Visiteur() { Id = 6, Nom = "La Carotte" };
            visiteurs.Add(visiteur);
            visiteur = new Visiteur() { Id = 7, Nom = "M. Potiron" };
            visiteurs.Add(visiteur);
            visiteur = new Visiteur() { Id = 8, Nom = "Mme Pirate" };
            visiteurs.Add(visiteur);
            visiteur = new Visiteur() { Id = 9, Nom = "Miss Ligulette" };
            visiteurs.Add(visiteur);
            visiteur = new Visiteur() { Id = 10, Nom = "Perle de lune" };
            visiteurs.Add(visiteur);
            visiteur = new Visiteur() { Id = 11, Nom = "M. Bond" };
            visiteurs.Add(visiteur);

        }
        #endregion
    }
}
