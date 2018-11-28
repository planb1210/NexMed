using Autofac;
using NexMed.Data;
using NexMed.Messaging;
using NexMed.WeatherServices;
using System;
using System.Linq;
using System.ServiceProcess;

namespace NexMed.WeatherWindowsService
{
    public partial class UpdateWeatherService : ServiceBase
    {
        private MailService ms;
        private NexMedContext db;
        private WeatherService ws;
        private NexMed.WeatherServices.UpdateWeatherService updateWeatherService;

        public UpdateWeatherService()
        {
            InitializeComponent();
            
            IContainer countainer = ContainerConfig.ConfigureContainer();
            var ms = countainer.Resolve<MailService>();
            var db = countainer.Resolve<NexMedContext>();
            var ws = countainer.Resolve<WeatherService>();
            updateWeatherService = new NexMed.WeatherServices.UpdateWeatherService(db, ms, ws);            
        }

        protected override void OnStart(string[] args)
        {            
            updateWeatherService.StartObservation();
        }

        protected override void OnStop()
        {
            updateWeatherService.StopObservation();
        }
    }
}
