using Newtonsoft.Json;

namespace cryptoTrading.AggregatePrice.Application.Models
{
    public class HoubiResponse
    {
        [JsonProperty("data")]
        public required List<HoubiTicker> Data { get; set; }
    }

    public class HoubiTicker
    {
        [JsonProperty("symbol")]
        public required string Symbol { get; set; }

        [JsonProperty("bid")]
        public decimal Bid { get; set; }

        [JsonProperty("ask")]
        public decimal Ask { get; set; }
    }
}
