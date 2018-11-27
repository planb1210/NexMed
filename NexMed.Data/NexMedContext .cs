using NexMed.Entities;
using System.Data.Entity;

namespace NexMed.Data
{
    public class NexMedContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Weather> Weathers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);

            modelBuilder.Entity<City>().HasKey(x => x.Id);

            modelBuilder.Entity<Weather>().HasKey(x => x.Id);
        }
    }
}
