using TheBookClinic.Messaging.Commands;

namespace TheBookClinic.StateSaga.Commands
{
    public class PriceRequestedCommand : IPriceRequestedCommand
    {
        public string TradeId { get; }

        public PriceRequestedCommand(string tradeId)
        {
            this.TradeId = tradeId;
        }
    }
}