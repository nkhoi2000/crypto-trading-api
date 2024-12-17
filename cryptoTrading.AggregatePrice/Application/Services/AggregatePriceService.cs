using cryptoTrading.AggregatePrice.Application.Interfaces;
using cryptoTrading.AggregatePrice.Application.Models;
using cryptoTrading.AggregatePrice.Domain.Entities;
using cryptoTrading.AggregatePrice.Infrastructure.Data;
using System.Net.WebSockets;

namespace cryptoTrading.AggregatePrice.Application.Services
{
    public class AggregatePriceService : IPriceService
    {
        private readonly IEnumerable<IBinanceSource> _binanceSource;
        private readonly IEnumerable<IHoubiSource> _houbiSource;
        private readonly CryptoTradingDbContext _tradingDbContext;
        public AggregatePriceService(
            IEnumerable<IBinanceSource> priceSource,
            IEnumerable<IHoubiSource> houbiSources,
            CryptoTradingDbContext tradingDbContext)
        {
            _binanceSource = priceSource;
            _tradingDbContext = tradingDbContext;
            _houbiSource = houbiSources;
        }

        public async Task FetchAndAggregatePrices()
        {
            var allPrice = new List<PriceData>();
            foreach (var source in _houbiSource)
            {
                var prices = await source.GetPriceAsync();
                allPrice.AddRange(prices);
            }

            foreach (var source in _binanceSource)
            {
                var prices = await source.GetPriceAsync();
                allPrice.AddRange(prices);
            }
            
            var pairs = new[] { "ETHUSDT", "BTCUSDT" };
            foreach (var pair in pairs) {
                var pairPrice = allPrice.Where(p => p.TradingPair == pair);
                if (pairPrice.Any())
                {
                    var bestBid = pairPrice.Max(p => p.BidPrice);
                    var bestAsk = pairPrice.Min(p => p.AskPrice);
                    var aggregatePrice = new AggregatedPrice
                    {
                        TradingPair = pair,
                        BidPrice = bestBid,
                        AskPrice = bestAsk,
                        Timestamp = DateTime.UtcNow
                    };
                    _tradingDbContext.AggregatePrices.Add(aggregatePrice);
                }
            }
            await _tradingDbContext.SaveChangesAsync();
        }
    }
}
