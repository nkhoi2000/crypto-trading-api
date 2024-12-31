namespace cryptoTrading.AggregatePrice.Domain.Entities
{
    public class AggregatedPrice
    {
        public int Id { get; set; }
        public required string TradingPair { get; set; }
        public decimal BestBid { get; set; }
        public string? BidSource { get; set; }
        public decimal BestAsk { get; set; }
        public string? AskSource { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
