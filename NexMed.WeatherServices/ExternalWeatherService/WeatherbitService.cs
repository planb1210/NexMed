﻿using Newtonsoft.Json;
using NexMed.Data;
using NexMed.Entities;
using NexMed.WeatherServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NexMed.WeatherServices
{
    public class WeatherbitService : IWeatherService
    {
        private const string aPIKey = "58c2500edd1b4308aa4bf5063a7fcb03";

        private const string httpUrl = "http://api.weatherbit.io/v2.0/current";

        private readonly HttpClient client = new HttpClient();

        public async Task<Weather> GetCityWeather(City city)
        {
            var lat = city.Latitude.ToString().Replace(",", ".");
            var lon = city.Longitude.ToString().Replace(",", ".");

            var url = $"{httpUrl}?key={aPIKey}&lat={lat}&lon={lon}";

            using (var response = await client.GetAsync(url))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return ParseJsonToWeather(responseString, city);
            }            
        }

        private Weather ParseJsonToWeather(string json, City city)
        {
            var weather = JsonConvert.DeserializeObject<WeatherbitModel>(json);
            if (weather.Data != null)
            {
                return new Weather()
                {
                    City = city,
                    Pressure = weather.Data[0].Pres,
                    Temperature = weather.Data[0].Temp,
                    WindSpeed = weather.Data[0].Wind_spd
                };
            }
            return null;
        }
    }
}
