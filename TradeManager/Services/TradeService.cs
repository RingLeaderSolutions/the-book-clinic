using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TradeManager.Common.Domain;
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
            Console.WriteLine($" - Looking for '{tradeName}' in CRM.");
            Thread.Sleep(1000);
            Console.WriteLine($" - Trade '{tradeName}' has been found in Salesforce.");
            return new EnrichedTrade
            {
                Name = tradeName
            };
        }


        private TradePrice RequestPrice(EnrichedTrade trade)
        {
            Console.WriteLine($" - Price for trade '{trade.Name}' has been requested.");
            Thread.Sleep(2000);
            Console.WriteLine($" - Price for trade '{trade.Name}' received.");
            return new TradePrice();
        }


        private void AddTradeToRms(EnrichedTrade trade, TradePrice price)
        {
            Console.WriteLine($" - Sending '{trade.Name}' to Risk Management System.");
            Thread.Sleep(1000);
            Console.WriteLine($" - Trade '{trade.Name}' pushed to Risk Management System.");
        }
    }
}