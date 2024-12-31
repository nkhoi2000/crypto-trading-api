using cryptoTrading.AggregatePrice.Application.Models;
using cryptoTrading.AggregatePrice.Domain.Entities;

namespace cryptoTrading.AggregatePrice.Application.Interfaces
{
    public interface IPriceService
    {
        Task FetchAndAggregatePrices();
        Task<IEnumerable<AggregatedPrice>> GetAllPricesAsync();
    }
}
