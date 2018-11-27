using NexMed.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexMed.WeatherServices
{
    public class WeatherService
    {
        private IEnumerable<IWeatherService> weatherServices;

        public WeatherService(IEnumerable<IWeatherService> allweatherService)
        {
            weatherServices = allweatherService;
        }

        public async Task<Weather> GetCityWeather(int cityId)
        {
            foreach (var service in weatherServices)
            {
                var weather = await service.GetCityWeather(cityId);
                if (weather != null) {
                    return weather;
                }
            }
            return null;
        }
    }
}
