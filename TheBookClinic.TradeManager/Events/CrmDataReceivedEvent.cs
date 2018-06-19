using TheBookClinic.Messaging.Events;

namespace TheBookClinic.TradeManager.Events
{
    public class CrmDataReceivedEvent : ICrmDataReceivedEvent
    {
        public string TradeId { get; set; }

        public CrmDataReceivedEvent(string tradeId)
        {
            TradeId = tradeId;
        }
    }
}