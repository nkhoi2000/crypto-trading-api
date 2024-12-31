namespace cryptoTrading.AggregatePrice.Application.Models
{
    public class PriceData
    {
        public required string TradingPair {  get; set; }
        public decimal BidPrice { get; set; }
        public decimal AskPrice { get; set; }
        public string? Source { get; set; }
    }
}
