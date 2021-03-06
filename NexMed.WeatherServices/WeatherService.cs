﻿using NexMed.Entities;
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

        public WeatherService(IEnumerable<IWeatherService> allWeatherService)
        {
            weatherServices = allWeatherService;
        }

        public async Task<Weather> GetCityWeather(City city)
        {
            foreach (var service in weatherServices)
            {
                var weather = await service.GetCityWeather(city);
                if (weather != null) {
                    return weather;
                }
            }
            return null;
        }
    }
}
