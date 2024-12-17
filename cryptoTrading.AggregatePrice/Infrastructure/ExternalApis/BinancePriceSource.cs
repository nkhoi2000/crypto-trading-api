using cryptoTrading.AggregatePrice.Application.Interfaces;
using cryptoTrading.AggregatePrice.Application.Models;
using Newtonsoft.Json;

namespace cryptoTrading.AggregatePrice.Infrastructure.ExternalApis
{
    public class BinancePriceSource : IBinanceSource
    {
        private readonly HttpClient _httpClient;
        public BinancePriceSource(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PriceData>> GetPriceAsync()
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
    }
}
