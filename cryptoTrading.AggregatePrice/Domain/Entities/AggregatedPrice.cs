namespace cryptoTrading.AggregatePrice.Domain.Entities
{
    public class AggregatedPrice
    {
        public int Id { get; set; }
        public string TradingPair { get; set; }
        public decimal BidPrice { get; set; }
        public decimal AskPrice { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
