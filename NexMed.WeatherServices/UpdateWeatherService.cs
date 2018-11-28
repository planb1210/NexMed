using NexMed.Data;
using NexMed.Entities;
using NexMed.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NexMed.WeatherServices
{
    public class UpdateWeatherService
    {
        private NexMedContext db;
        private MessageService ms;
        private WeatherService ws;
        private CancellationTokenSource cancelTokenSource;

        public UpdateWeatherService(NexMedContext Db, MessageService Ms, WeatherService Ws)
        {
            db = Db;
            ms = Ms;
            ws = Ws;
        }

        public void StartObservation()
        {
            cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            TimeSpan interval = new TimeSpan(1000);

            RunPeriodicallyAsync(WeatherObserver, interval, token);
        }

        public void StopObservation()
        {
            cancelTokenSource.Cancel();
        }

        private async Task RunPeriodicallyAsync(Func<Task> func, TimeSpan interval, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(interval, cancellationToken);
                await func();
            }
        }

        private async Task WeatherObserver()
        {
            var cities = db.Cities.ToList();
           
            foreach (var city in cities)
            {
                var weather = await ws.GetCityWeather(city);
                var currentWeather = db.Weathers.Where(x => x.City.Id == weather.City.Id).FirstOrDefault();
                if (currentWeather != null)
                {
                    if (weather.Pressure != currentWeather.Pressure || weather.Temperature != currentWeather.Temperature || weather.WindSpeed != currentWeather.WindSpeed)
                    {
                        currentWeather.Pressure = weather.Pressure;
                        currentWeather.Temperature = weather.Temperature;
                        currentWeather.WindSpeed = weather.WindSpeed;
                        db.SaveChanges();
                        var usersWithCity = db.Users.Where(x => x.City.Id == currentWeather.City.Id).ToList();
                        ms.SendEmails(usersWithCity, currentWeather);
                    }
                }
                else
                {
                    db.Weathers.Add(weather);
                    db.SaveChanges();
                    var usersWithCity = db.Users.Where(x => x.City.Id == weather.City.Id).ToList();
                    ms.SendEmails(usersWithCity, weather);
                }
            }
        }
    }
}
