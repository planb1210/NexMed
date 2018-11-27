using Autofac;
using NexMed.Data;
using NexMed.Messaging;
using NexMed.WeatherServices;
using System;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace NexMed.WeatherWindowsService
{
    public partial class UpdateWeatherService : ServiceBase
    {
        private Autofac.IContainer countainer;
        private CancellationTokenSource cancelTokenSource;

        public UpdateWeatherService()
        {
            InitializeComponent();
        }

        public async Task RunPeriodicallyAsync(
            Func<Task> func,
            TimeSpan interval,
            CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(interval, cancellationToken);
                await func();
            }
        }

        protected override void OnStart(string[] args)
        {
            cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            TimeSpan interval = new TimeSpan(100);

            RunPeriodicallyAsync(WeatherObserver, interval, token);
        }

        protected override void OnStop()
        {
            cancelTokenSource.Cancel();
        }

        public async Task WeatherObserver()
        {
            var ms = countainer.Resolve<MessageService>();
            var db = countainer.Resolve<NexMedContext>();
            var weatherServices = countainer.Resolve<WeatherService>();

            var cities = db.Cities;
            foreach (var city in cities)
            {
                var weather = await weatherServices.GetCityWeather(city.Id);
                var currentWeather = db.Weathers.Where(x => x.City == weather.City).FirstOrDefault();
                if (currentWeather != null)
                {
                    if (weather.Pressure != currentWeather.Pressure || weather.Temperature != currentWeather.Temperature || weather.WindSpeed != currentWeather.WindSpeed)
                    {
                        currentWeather.Pressure = weather.Pressure;
                        currentWeather.Temperature = weather.Temperature;
                        currentWeather.WindSpeed = weather.WindSpeed;
                        db.SaveChanges();
                        var usersWithCity = db.Users.Where(x => x.City == currentWeather.City).ToList();
                        ms.SendEmails(usersWithCity, currentWeather);
                    }
                }
                else
                {
                    db.Weathers.Add(weather);
                    db.SaveChanges();
                    var usersWithCity = db.Users.Where(x => x.City == weather.City).ToList();
                    ms.SendEmails(usersWithCity, weather);
                }
            }
        }
    }
}
