using EscarGo.Models;
using System.Data.Entity;

namespace EscarGo.Repositories
{
    sealed class EscarGoContext : DbContext
    {
        public EscarGoContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Concurrent> Concurrents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                        .HasMany(s => s.Concurrents)
                        .WithMany(c => c.Courses);

            modelBuilder.Entity<Concurrent>()
              .HasMany(s => s.Courses)
              .WithMany(c => c.Concurrents);

        }
    }
}
