using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Serilog;
using TheBookClinic.Common;
using Topshelf;
using Topshelf.Autofac;

namespace TheBookClinic.PricingService
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggingConfig = LoggingConfiguration.ConfigureLogger("PricingService");
            HostFactory.Run(cfg =>
            {
                var container = Initialise();

                cfg.UseSerilog(loggingConfig);
                cfg.UseAutofacContainer(container);

                cfg.Service<PricingService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted((service, control) => service.Start(control));
                    s.WhenStopped((service, control) => service.Stop(control));
                });

                cfg.RunAsLocalService();
                cfg.SetDisplayName("Pricing Service");
                cfg.SetDescription("This service produces prices for trades.");

                cfg.OnException(ex =>
                {
                    Log.Fatal(ex, "Encountered unhandled exception (bubbled to Topshelf)");
                });
            });
        }


        private static IContainer Initialise()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new PricingServiceModule());

            return builder.Build();
        }
    }
}
