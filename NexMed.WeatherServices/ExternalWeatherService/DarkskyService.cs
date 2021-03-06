﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NexMed.Data;
using NexMed.Entities;
using NexMed.WeatherServices.Models;

namespace NexMed.WeatherServices
{
    public class DarkskyService: IWeatherService
    {
        private const string aPIKey = "a3f13c1314c1c3bbc71133199697a8f2";

        private const string httpUrl = "https://api.darksky.net/forecast";

        private readonly HttpClient client = new HttpClient();

        public async Task<Weather> GetCityWeather(City city)
        {
            var lat = city.Latitude.ToString().Replace(",", ".");
            var lon = city.Longitude.ToString().Replace(",", ".");

            var url = $"{httpUrl}/{aPIKey}/{lat},{lon}";

            using (var response = await client.GetAsync(url))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return ParseJsonToWeather(responseString, city);
            }
        }

        private Weather ParseJsonToWeather(string json, City city)
        {
            var weather = JsonConvert.DeserializeObject<DarkskyModel>(json);
            if (weather.Currently != null)
            {
                return new Weather()
                {
                    City = city,
                    Pressure = weather.Currently.Pressure,
                    Temperature = weather.Currently.Temperature,
                    WindSpeed = weather.Currently.WindSpeed
                };
            }
            return null;
        }
    }
}
