namespace NexMed.Data.Migrations
{
    using NexMed.Entities;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<NexMedContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NexMedContext context)
        {
            SeedCities(context);
        }

        private void SeedCities(NexMedContext context)
        {
            context.Cities.AddOrUpdate(x => x.Id, new City
            {
                Name = "Moscow",
                Latitude = 55.755826,
                Longitude = 37.617299900000035
            });

            context.Cities.AddOrUpdate(x => x.Id, new City
            {
                Name = "St. Petersburg",
                Latitude = 59.9342802,
                Longitude = 30.335098600000038
            });

            context.Cities.AddOrUpdate(x => x.Id, new City
            {
                Name = "Novosibirsk",
                Latitude = 55.00835259999999,
                Longitude = 82.93573270000002
            });
        }
    }
}
