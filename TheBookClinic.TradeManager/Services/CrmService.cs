using System.Threading.Tasks;
using MassTransit;
using Serilog;
using TheBookClinic.Messaging.Commands;
using TheBookClinic.Messaging.Events;
using TheBookClinic.TradeManager.Events;

namespace TheBookClinic.TradeManager.Services
{
    public class CrmService : IConsumer<ICrmDataRequestedCommand>
    {
        public async Task Consume(ConsumeContext<ICrmDataRequestedCommand> context)
        {
            var tradeId = context.Message.TradeId;
            Log.Information("[CRM Service] Received data request for Trade: TradeId=[{TradeId}]", tradeId);
            await context.Publish<ICrmDataReceivedEvent>(new CrmDataReceivedEvent(tradeId));
        }
    }
}