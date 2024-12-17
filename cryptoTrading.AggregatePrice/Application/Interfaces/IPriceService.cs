using cryptoTrading.AggregatePrice.Application.Models;

namespace cryptoTrading.AggregatePrice.Application.Interfaces
{
    public interface IPriceService
    {
        Task FetchAndAggregatePrices();
    }
}
