using TheBookClinic.Messaging.Events;

namespace TheBookClinic.TradeManager.Events
{
    public class NewTradeReceived : INewTradeReceived
    {
        public string TradeId { get; set; }

        public NewTradeReceived(string tradeId)
        {
            TradeId = tradeId;
        }
    }
}