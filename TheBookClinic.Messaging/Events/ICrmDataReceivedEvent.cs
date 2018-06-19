namespace TheBookClinic.Messaging.Events
{
    public interface ICrmDataReceivedEvent
    {
        string TradeId { get; set; }
    }
}