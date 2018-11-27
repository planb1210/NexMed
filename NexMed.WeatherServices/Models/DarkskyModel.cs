using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexMed.WeatherServices.Models
{
    public class DarkskyModel
    {
        public DarkskyCurrently Currently { get; set; }
    }

    public class DarkskyCurrently
    {
        private string temperature;
        private string windSpeed;
        private string pressure;

        public string Temperature
        {
            get
            {
                return temperature + " C";
            }
            set
            {
                temperature = value;
            }
        }

        public string WindSpeed
        {
            get
            {
                return windSpeed + " m/s";
            }
            set
            {
                windSpeed = value;
            }
        }

        public string Pressure
        {
            get
            {
                return pressure + " mb";
            }
            set
            {
                pressure = value;
            }
        }
    }
}
