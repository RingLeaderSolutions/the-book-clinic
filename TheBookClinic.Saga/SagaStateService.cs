using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.EntityFrameworkIntegration;
using MassTransit.EntityFrameworkIntegration.Saga;
using MassTransit.Saga;
using TheBookClinic.Messaging.MassTransit;
using TheBookClinic.Persistence.DbContext;
using TheBookClinic.Persistence.Model;
using TheBookClinic.StateSaga.StateMachine;
using Topshelf;

namespace TheBookClinic.StateSaga
{
    internal class SagaStateService : ServiceControl
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
            var quoteStateMachine = new TradeStateMachine();

            SagaDbContextFactory quoteDbContextFactory =
                () => new PersistedTradeDbContext();

            var quoteRepo = new Lazy<ISagaRepository<PersistedTrade>>(
                () => new EntityFrameworkSagaRepository<PersistedTrade>(quoteDbContextFactory));


            return BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(
                    host,
                    "trade_saga",
                    e =>
                    {
                        e.UseInMemoryOutbox();
                        e.StateMachineSaga(quoteStateMachine, quoteRepo.Value);
                    });
            });
        }

    }
}