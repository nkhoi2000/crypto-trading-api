using cryptoTrading.AggregatePrice.Application.Interfaces;
using cryptoTrading.AggregatePrice.Application.Models;
using Newtonsoft.Json;

namespace cryptoTrading.AggregatePrice.Infrastructure.ExternalApis
{
    public class HoubiPriceSource : IHoubiSource
    {
        private readonly HttpClient _httpClient;
        public HoubiPriceSource(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PriceData>> GetPriceAsync()
        {
            var response = await _httpClient.GetStringAsync("https://api.huobi.pro/market/tickers");
            var prices = JsonConvert.DeserializeObject<List<dynamic>>(response);

            return prices.Select(p => new PriceData
            {
                TradingPair = p.symbol,
                BidPrice = decimal.Parse((string)p.bid),
                AskPrice = decimal.Parse((string)p.ask),
            });
        }
    }
}
