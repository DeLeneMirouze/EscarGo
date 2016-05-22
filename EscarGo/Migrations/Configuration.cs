namespace EscarGo.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EscarGo.Repositories.EscarGoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EscarGo.Repositories.EscarGoContext context)
        {
            //  This method will be called after migrating to the latest version.
            Random rnd = new Random(DateTime.Now.Millisecond);

            Concurrent[] concurrents = new Concurrent[]
            {
                    new Concurrent { Nom = "Speedy Jet Trophy", Victoires = 100, Defaites = 0, Entraineur = "Dr YaCommeUneMagouille",IdConcurrent=1 },
                new Concurrent { Nom = "Spidi Gonzales", Victoires = 0, Defaites = 100, Entraineur = "Papy Emile",IdConcurrent=2 },
                   new Concurrent { Nom = "La Foudre du Nord", Victoires = 2, Defaites = 8, Entraineur = "Moumoune",IdConcurrent=3 },
                        new Concurrent { Nom = "Cool Man", Victoires = 50, Defaites = 2, Entraineur = "Paprika" ,IdConcurrent=4},
                             new Concurrent { Nom = "Salade", Victoires = 5, Defaites = 6, Entraineur = "La Carotte",IdConcurrent=5 },
                                  new Concurrent { Nom = "Super Gascon", Victoires = 3, Defaites = 3, Entraineur = "Patchouli" ,IdConcurrent=6},
     new Concurrent { Nom = "Gros Baveux", Victoires = 45, Defaites = 5, Entraineur = "Paprika",IdConcurrent=7 },
          new Concurrent { Nom = "Petit Baveux", Victoires = 160, Defaites = 55, Entraineur = "La Carotte" ,IdConcurrent=8},
               new Concurrent { Nom = "Persillade", Victoires = 2, Defaites = 5, Entraineur = "Paprika",IdConcurrent=9 },
                    new Concurrent { Nom = "Doudou", Victoires = 2, Defaites = 3, Entraineur = "Paprika" ,IdConcurrent=10},
                         new Concurrent { Nom = "Pilou Pilou", Victoires = 3, Defaites = 8, Entraineur = "Patchouli" ,IdConcurrent=11}
            };

            foreach (Concurrent item in concurrents)
            {
                // les paris
                Pari pari = new Pari();
                pari.DateDernierPari = DateTime.Now;
                pari.Montants = 1 + rnd.Next(50);
                pari.NbParis = 1 + rnd.Next(6);
                item.Pari = pari;
            }


            int[] years = Enumerable.Range(DateTime.Now.Year - 2, 4).ToArray();

            List<Course> courses = new List<Course>() {   new Course { Label = "Speedy Jet Trophy", Pays="Des Merveilles",Ville="Xanadu" },
              new Course { Label = "Restau de la Gare", Pays = "France", Ville = "Bapaume (sud)" },
              new Course { Label = "Cassoulet Lillois", Pays = "France", Ville = "Wazemmes" },
                     new Course { Label = "La Grande Course du Large", Pays = "Suisse", Ville = "Berne" },
                            new Course { Label = "Cache-cache", Pays = "Belgique", Ville = "Bruxelles" },
                                   new Course { Label = "Attrap'moi", Pays = "Tunisie", Ville = "Tunis" },
                                          new Course { Label = "Rally sans Fronti�re", Pays = "France", Ville="Paris" },
                                           new Course { Label = "La Grande Course", Pays = "Suisse",Ville="Bernes" },
                                            new Course { Label = "Radis et Salade", Pays = "Portugal", Ville = "Porto" }};


            int idCourse = 1;
            int[] myValues = Enumerable.Range(0, concurrents.Length).ToArray();
            List<Course> listeCourses = new List<Course>();
            foreach (int year in years)
            {
                foreach (Course course in courses)
                {
                    if (rnd.NextDouble() < 0.1)
                    {
                        // la course n'a pas eu lieu
                        continue;
                    }

                    Course c = new Course();
                    c.Concurrents = new List<Concurrent>();
                    c.Label = course.Label;
                    c.Pays = course.Pays;
                    c.Ville = course.Ville;
                    c.IdCourse = idCourse;
                    idCourse++;

                    // quand a lieu la course cette ann�e l�?
                    c.Date = new DateTime(year, 1 + rnd.Next(12), 1 + rnd.Next(28));

                    // qui participait? Il en faut au moins 2
                    int nbParticipants = 2 + rnd.Next(concurrents.Length - 1);
                    int[] threeRandom = myValues.OrderBy(x => rnd.Next()).Take(nbParticipants).ToArray();
                    for (int i = 0; i < threeRandom.Length; i++)
                    {
                        int valeur = threeRandom[i];
                        c.Concurrents.Add(concurrents[valeur]);
                        concurrents[valeur].Courses.Add(c);
                    }
         

                    listeCourses.Add(c);
                }
            }

            context.Courses.AddOrUpdate(p => p.IdCourse, listeCourses.ToArray());
            context.Concurrents.AddOrUpdate(p => p.IdConcurrent, concurrents);
        }
    }
}
