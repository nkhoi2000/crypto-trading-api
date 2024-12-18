using cryptoTrading.AggregatePrice.Application.Interfaces;
using cryptoTrading.AggregatePrice.Application.Models;
using Newtonsoft.Json;

namespace cryptoTrading.AggregatePrice.Infrastructure.ExternalApis
{
    public class BinancePriceSource : IBinanceSource
    {
        private readonly HttpClient _httpClient;
        private ILogger<BinancePriceSource> _logger;
        public BinancePriceSource(HttpClient httpClient, ILogger<BinancePriceSource> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<PriceData>> GetPriceAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://api.binance.com/api/v3/ticker/bookTicker");
                var prices = JsonConvert.DeserializeObject<List<dynamic>>(response);

                return prices.Select(p => new PriceData
                {
                    TradingPair = p.symbol,
                    BidPrice = decimal.Parse((string)p.bidPrice),
                    AskPrice = decimal.Parse((string)p.askPrice),
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong when fetching data from the source: {Message}", ex.Message);
                throw new Exception("Failed to fetch price data from Binance source.", ex);
            }
        }
    }
}
