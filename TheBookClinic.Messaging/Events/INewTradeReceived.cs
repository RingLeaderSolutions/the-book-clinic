namespace TheBookClinic.Messaging.Events
{
    public interface INewTradeReceived
    {
        string TradeId { get; set; }
    }
}