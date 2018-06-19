using TheBookClinic.Messaging.Events;

namespace TheBookClinic.TradeManager.Events
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