using Autofac;
using NexMed.Data;
using NexMed.WeatherServices;
using System.Collections.Generic;

namespace NexMed.WeatherWindowsService
{
    public class ContainerConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<NexMedContext>().AsSelf();

            builder.RegisterType<DarkskyService>().As<IWeatherService>();
            builder.RegisterType<WeatherbitService>().As<IWeatherService>();
            builder.RegisterType<WeatherService>().AsSelf();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var allweatherService = scope.Resolve<IEnumerable<IWeatherService>>();
            }

            return container;
        }
    }
}