using System;
using System.Threading.Tasks;
using Autofac;
using Serilog;
using TheBookClinic.Common;
using TheBookClinic.TradeManager.Services;
using Topshelf;
using Topshelf.Autofac;

namespace TheBookClinic.TradeManager
{
    class Program
    {
        static void Main()
        {
            var loggingConfig = LoggingConfiguration.ConfigureLogger("TradeManager");
            HostFactory.Run(cfg =>
            {
                var container = Initialise();

                cfg.UseSerilog(loggingConfig);
                cfg.UseAutofacContainer(container);

                cfg.Service<TradeService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted((service, control) => service.Start(control));
                    s.WhenStopped((service, control) => service.Stop(control));
                });
                
                cfg.RunAsLocalService();
                cfg.SetDisplayName("Trade Manager Service");
                cfg.SetDescription("This service facilitates the handling trades.");

                cfg.OnException(ex =>
                {
                    Log.Fatal(ex, "Encountered unhandled exception (bubbled to Topshelf)");
                });
            });
        }


        private static IContainer Initialise()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new TradeManagerModule());

            return builder.Build();
        }
    }
}
