using System.Threading.Tasks;
using MassTransit;
using Serilog;
using TheBookClinic.Messaging.Commands;
using TheBookClinic.Messaging.Events;
using TheBookClinic.TradeManager.Events;

namespace TheBookClinic.TradeManager.Services
{
    public class PricingService : IConsumer<IPriceRequestedCommand>
    {
        public async Task Consume(ConsumeContext<IPriceRequestedCommand> context)
        {
            var tradeId = context.Message.TradeId;
            Log.Information("[Pricing Service] Received price request for Trade: TradeId=[{TradeId}]", tradeId);
            await context.Publish<IPriceReceivedEvent>(new PriceReceivedEvent(tradeId));
        }
    }
}