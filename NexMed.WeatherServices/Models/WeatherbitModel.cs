using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexMed.WeatherServices.Models
{
    public class WeatherbitModel
    {
        public WeatherbitData[] Data { get; set; }
    }

    public class WeatherbitData
    {
        private string temp;
        private string wind_spd;
        private string pres;

        public string Temp
        {
            get {
                return temp + " C";
            }
            set
            {
                temp = value;
            }
        }

        public string Wind_spd
        {
            get
            {
                return wind_spd + " m/s";
            }
            set
            {
                wind_spd = value;
            }
        }

        public string Pres
        {
            get
            {
                return pres + " mb";
            }
            set
            {
                pres = value;
            }
        }
    }
}
