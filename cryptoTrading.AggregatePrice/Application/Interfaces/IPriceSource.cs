using System;
using cryptoTrading.AggregatePrice.Application.Models;

namespace cryptoTrading.AggregatePrice.Application.Interfaces;

public interface IPriceSource
{
    Task<IEnumerable<PriceData>> GetPriceAsync();
}
