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
        private MailService mailService;
        private WeatherService weatherService;
        private CancellationTokenSource cancelTokenSource;

        public UpdateWeatherService(NexMedContext Db, MailService MailService, WeatherService WeatherService)
        {
            db = Db;
            mailService = MailService;
            weatherService = WeatherService;
        }

        public void StartObservation()
        {
            cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            TimeSpan interval = new TimeSpan(1000);

            RunPeriodicallyAsync(interval, token);
        }

        public void StopObservation()
        {
            cancelTokenSource.Cancel();
        }

        private async Task RunPeriodicallyAsync(TimeSpan interval, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(interval, cancellationToken);
                await WeatherObserver(cancellationToken);
             }
        }

        private async Task WeatherObserver(CancellationToken cancellationToken)
        {
            var cities = db.Cities.ToList();
           
            foreach (var city in cities)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                var weather = await weatherService.GetCityWeather(city);
                var currentWeather = db.Weathers.FirstOrDefault(x => x.City.Id == weather.City.Id);
                if (currentWeather != null)
                {
                    if (weather.Pressure != currentWeather.Pressure || weather.Temperature != currentWeather.Temperature || weather.WindSpeed != currentWeather.WindSpeed)
                    {
                        currentWeather.Pressure = weather.Pressure;
                        currentWeather.Temperature = weather.Temperature;
                        currentWeather.WindSpeed = weather.WindSpeed;
                        db.SaveChanges();
                        var usersWithCity = db.Users.Where(x => x.City.Id == currentWeather.City.Id).ToList();
                        mailService.SendEmails(usersWithCity, currentWeather);
                    }
                }
                else
                {
                    db.Weathers.Add(weather);
                    db.SaveChanges();
                    var usersWithCity = db.Users.Where(x => x.City.Id == weather.City.Id).ToList();
                    mailService.SendEmails(usersWithCity, weather);
                }
            }
        }
    }
}
