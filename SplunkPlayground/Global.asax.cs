using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Serilog;
using Splunk.DAL;

namespace SplunkPlayground
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();


            builder.Register<ILogger>((c, p) =>
            {
                return new LoggerConfiguration()
                .MinimumLevel.Debug()
                //.WriteTo.LiterateConsole()
                .WriteTo.EventCollector(
                    "http://localhost:8088/services/collector",
                    "4B186B35-4DB3-4173-AA0E-1623B58E9256",
                    source: "Serilog.Sinks.Splunk.Sample.TestSource")
                .Enrich.WithProperty("Serilog.Sinks.Splunk.Sample", "ViaEventCollector")
                .Enrich.WithProperty("Serilog.Sinks.Splunk.Sample.TestType", "Source Override")
                .CreateLogger();
            }).SingleInstance();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterType<SplunkDbContext>().InstancePerRequest();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
