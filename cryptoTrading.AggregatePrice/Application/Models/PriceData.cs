namespace cryptoTrading.AggregatePrice.Application.Models
{
    public class PriceData
    {
        public string TradingPair {  get; set; }
        public decimal BidPrice { get; set; }
        public decimal AskPrice { get; set; }
    }
}
