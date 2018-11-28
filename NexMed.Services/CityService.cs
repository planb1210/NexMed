using NexMed.Data;
using NexMed.Entities;
using System.Collections.Generic;
using System.Linq;

namespace NexMed.Services
{
    public class CityService
    {
        private NexMedContext db;

        public CityService(NexMedContext context)
        {
            db = context;
        }

        public IEnumerable<City> GetCities()
        {
            return db.Cities;
        }

        public City GetCity(int cityId)
        {
            return db.Cities.Where(x => x.Id == cityId).FirstOrDefault();
        }
    }
}
