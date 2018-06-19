using TheBookClinic.Messaging.Commands;

namespace TheBookClinic.StateSaga.Commands
{
    public class CrmDataRequestedCommand : ICrmDataRequestedCommand
    {
        public string TradeId { get; }

        public CrmDataRequestedCommand(string tradeId)
        {
            this.TradeId = tradeId;
        }
    }
}