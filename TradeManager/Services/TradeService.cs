using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TradeManager.Domain;
using TradeManager.Repositories;

namespace TradeManager.Services
{
    internal class TradeService
    {
        private readonly TradeRepository _tradeRepository;

        public TradeService(TradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }


        public async Task<bool> AcceptTrade(string tradeName)
        {
            var trade = FindTrade(tradeName);

            if (!IsTradeValid(trade, tradeName)) return false;

            var enrichedTrade = EnrichTrade(tradeName);

            var price = RequestPrice(enrichedTrade);

            AddTradeToRms(enrichedTrade, price);

            Console.WriteLine($"Trade '{tradeName}' is now been accepted.");
            return true;
        }


        private Trade FindTrade(string tradeName)
        {
            return _tradeRepository.GetTrades().SingleOrDefault(t => t.Name == tradeName);
        }


        public void ShowTrades()
        {
            var trades = _tradeRepository.GetTrades();
            Console.WriteLine($"The following trades are ready for acceptance: {string.Join(", ", trades.Select(t => t.Name))}");
        }


        private bool IsTradeValid(Trade trade, string tradeName)
        {
            if (trade != null) return true;

            Console.WriteLine($"Trade '{tradeName}' not found.");
            return false;

        }


        private EnrichedTrade EnrichTrade(string tradeName)
        {
            Console.WriteLine($" - Looking for '{tradeName}' CRM.");
            Thread.Sleep(1000);
            Console.WriteLine($" - Trade '{tradeName}' has been found in Salesforce.");
            return new EnrichedTrade();
        }


        private TradePrice RequestPrice(EnrichedTrade tradeName)
        {
            Console.WriteLine($" - Price for trade '{tradeName}' has been requested.");
            Thread.Sleep(2000);
            Console.WriteLine($" - Price for trade '{tradeName}' received.");
            return new TradePrice();
        }


        private void AddTradeToRms(EnrichedTrade tradeName, TradePrice price)
        {
            Console.WriteLine($" - Sending '{tradeName}' to Risk Management System.");
            Thread.Sleep(1000);
            Console.WriteLine($" - Trade '{tradeName}' pushed to Risk Management System.");
        }
    }
}