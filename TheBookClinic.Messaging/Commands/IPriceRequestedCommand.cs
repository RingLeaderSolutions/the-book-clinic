namespace TheBookClinic.Messaging.Commands
{
    public interface IPriceRequestedCommand
    {
        string TradeId { get; }
    }
}