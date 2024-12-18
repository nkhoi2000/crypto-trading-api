using cryptoTrading.AggregatePrice.Application.Interfaces;
using cryptoTrading.AggregatePrice.Application.Models;
using Newtonsoft.Json;

namespace cryptoTrading.AggregatePrice.Infrastructure.ExternalApis
{
    public class HoubiPriceSource : IHoubiSource
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HoubiPriceSource> _logger;
        public HoubiPriceSource(HttpClient httpClient, ILogger<HoubiPriceSource> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<PriceData>> GetPriceAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://api.huobi.pro/market/tickers");
                var prices = JsonConvert.DeserializeObject<HoubiResponse>(response);

                return prices.Data.Select(p => new PriceData
                {
                    TradingPair = p.Symbol.ToUpper(),
                    BidPrice = p.Bid,
                    AskPrice = p.Ask
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
