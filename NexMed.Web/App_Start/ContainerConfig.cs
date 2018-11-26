using Autofac;
using System.Web.Mvc;
using NexMed.Data;
using System.Data.Entity;
using Autofac.Integration.Mvc;

namespace NexMed.Web.App_Start
{
    public class ContainerConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<NexMedContext>().AsSelf();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}