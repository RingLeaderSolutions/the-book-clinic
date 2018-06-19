using TheBookClinic.Messaging.Events;

namespace TheBookClinic.PricingService.Events
{
    public class PriceReceivedEvent : IPriceReceivedEvent
    {
        public string TradeId { get; set; }

        public PriceReceivedEvent(string tradeId)
        {
            TradeId = tradeId;
        }
    }
}