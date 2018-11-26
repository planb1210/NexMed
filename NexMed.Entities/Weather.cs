using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexMed.Entities
{
    public class Weather
    {
        public int Id { get; set; }

        public string CityName { get; set; }

        public string Temperature { get; set; }

        public string WindSpeed { get; set; }

        public string Pressure { get; set; }
    }
}
