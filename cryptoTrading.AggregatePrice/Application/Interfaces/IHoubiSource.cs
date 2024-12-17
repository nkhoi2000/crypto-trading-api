using cryptoTrading.AggregatePrice.Application.Models;

namespace cryptoTrading.AggregatePrice.Application.Interfaces
{
    public interface IHoubiSource
    {
        public Task<IEnumerable<PriceData>> GetPriceAsync();
    }
}
