using System.Threading.Tasks;
using MassTransit;
using TheBookClinic.Messaging.MassTransit;
using Topshelf;

namespace TheBookClinic.PricingService
{
    public class PricingService : ServiceControl
    {
        private IBusControl _bus;

        public bool Start(HostControl hostControl)
        {
            Task.Factory.StartNew(
                () =>
                {
                    _bus = ConfigureBus();
                    _bus.Start();
                });

            return true;
        }

        
        public bool Stop(HostControl hostControl)
        {
            return true;
        }


        private IBusControl ConfigureBus()
        {
            return BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.UseSerilog();

                cfg.ReceiveEndpoint(
                    host,
                    "pricing_data",
                    endpointConfigurator =>
                    {
                        endpointConfigurator.Consumer(() => new PricingServiceConsumer());
                    }); ;
            });
        }
    }
}