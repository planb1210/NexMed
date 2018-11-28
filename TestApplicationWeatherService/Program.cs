using NexMed.Data;
using NexMed.Messaging;
using NexMed.WeatherServices;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplicationWeatherService
{
    public class Program
    {
        static void Main(string[] args)
        {
            IContainer countainer = ContainerConfig.ConfigureContainer();
            var ms = countainer.Resolve<MailService>();
            var db = countainer.Resolve<NexMedContext>();
            var ws = countainer.Resolve<WeatherService>();

            var updateWeatherService = new UpdateWeatherService(db, ms, ws);
            updateWeatherService.StartObservation();
            Console.ReadKey();
        }
    }
}
