
using TradeManager.Common.Domain;

namespace TradeManager.Repositories
{
    internal class TradeRepository
    {
        public Trade[] GetTrades()
        {
            return new[] {
                new Trade { Name= "Trade1"},
                new Trade { Name= "Trade2"},
                new Trade { Name= "Trade3" }
            };
        }
    }
}