namespace TheBookClinic.Messaging.Events
{
    public interface IPriceReceivedEvent
    {
        string TradeId { get; set; }
    }
}