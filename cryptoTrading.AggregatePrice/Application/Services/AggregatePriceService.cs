using cryptoTrading.AggregatePrice.Application.Interfaces;
using cryptoTrading.AggregatePrice.Application.Models;
using cryptoTrading.AggregatePrice.Domain.Entities;
using cryptoTrading.AggregatePrice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
            allPrice.AddRange(await FetchPricesFromSource(_houbiSource));
            allPrice.AddRange(await FetchPricesFromSource(_binanceSource));

            var pairs = new[] { "ETHUSDT", "BTCUSDT" };
            foreach (var pair in pairs)
            {
                var pairPrice = allPrice.Where(p => p.TradingPair == pair);
                if (pairPrice.Any())
                {
                    var bestBidPrice = pairPrice.OrderByDescending(p => p.BidPrice).First();
                    var bestAskPrice = pairPrice.OrderBy(p => p.AskPrice).First();
                    var aggregatePrice = new AggregatedPrice
                    {
                        TradingPair = pair,
                        BestBid = bestBidPrice.BidPrice,
                        BidSource = bestBidPrice.Source,
                        BestAsk = bestAskPrice.AskPrice,
                        AskSource = bestAskPrice.Source,
                        Timestamp = DateTime.UtcNow
                    };
                    _tradingDbContext.AggregatePrices.Add(aggregatePrice);
                }
            }
            await _tradingDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AggregatedPrice>> GetAllPricesAsync()
        {
            return await _tradingDbContext.AggregatePrices.ToListAsync();
        }

        private async Task<IEnumerable<PriceData>> FetchPricesFromSource(IEnumerable<IPriceSource> sources)
        {
            var prices = new List<PriceData>();
            foreach (var source in sources)
            {
                var sourcePrices = await source.GetPriceAsync();
                prices.AddRange(sourcePrices);
            }
            return prices;
        }
    }
}
