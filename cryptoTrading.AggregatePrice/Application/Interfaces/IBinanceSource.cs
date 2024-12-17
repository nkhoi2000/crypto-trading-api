using cryptoTrading.AggregatePrice.Application.Models;

namespace cryptoTrading.AggregatePrice.Application.Interfaces
{
    public interface IBinanceSource
    {
        public Task<IEnumerable<PriceData>> GetPriceAsync();
    }
}
