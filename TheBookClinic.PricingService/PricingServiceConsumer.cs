using System.Threading.Tasks;
using MassTransit;
using Serilog;
using TheBookClinic.Messaging.Commands;
using TheBookClinic.Messaging.Events;
using TheBookClinic.PricingService.Events;

namespace TheBookClinic.PricingService
{
    public class PricingServiceConsumer : IConsumer<IPriceRequestedCommand>
    {
        public async Task Consume(ConsumeContext<IPriceRequestedCommand> context)
        {
            var tradeId = context.Message.TradeId;
            Log.Information("[Pricing Service] Received price request for Trade: TradeId=[{TradeId}]", tradeId);
            await context.Publish<IPriceReceivedEvent>(new PriceReceivedEvent(tradeId));
        }
    }
}