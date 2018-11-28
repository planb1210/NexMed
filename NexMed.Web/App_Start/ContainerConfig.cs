using Autofac;
using System.Web.Mvc;
using NexMed.Data;
using Autofac.Integration.Mvc;
using NexMed.WeatherServices;
using System.Collections.Generic;
using NexMed.Services;

namespace NexMed.Web.App_Start
{
    public class ContainerConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<NexMedContext>().AsSelf();
            builder.RegisterType<UserService>().AsSelf();
            builder.RegisterType<CityService>().AsSelf();

            builder.RegisterType<DarkskyService>().As<IWeatherService>();
            builder.RegisterType<WeatherbitService>().As<IWeatherService>();
            builder.RegisterType<WeatherService>().AsSelf();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var allweatherService = scope.Resolve<IEnumerable<IWeatherService>>();
            }

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}