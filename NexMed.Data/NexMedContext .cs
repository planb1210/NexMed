using NexMed.Entities;
using System.Data.Entity;

namespace NexMed.Data
{
    public class NexMedContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
        }
    }
}
