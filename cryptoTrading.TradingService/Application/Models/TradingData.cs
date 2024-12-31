using System;

namespace cryptoTrading.TradingService.Application.Models;

public enum TradeType
{
    BUY,
    SELL
}
public class TradingData
{
    public int Id { get; set; }
    public required string TradingPair { get; set; }
    public decimal Amount { get; set; }
    public decimal Price {get ; set; }
    
}
