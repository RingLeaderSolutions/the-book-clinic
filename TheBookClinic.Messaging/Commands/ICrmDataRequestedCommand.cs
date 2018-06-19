namespace TheBookClinic.Messaging.Commands
{
    public interface ICrmDataRequestedCommand
    {
        string TradeId { get; }
    }
}