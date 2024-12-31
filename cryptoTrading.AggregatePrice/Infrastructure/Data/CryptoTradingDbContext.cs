using cryptoTrading.AggregatePrice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace cryptoTrading.AggregatePrice.Infrastructure.Data
{
    public class CryptoTradingDbContext(DbContextOptions<CryptoTradingDbContext> options) : DbContext(options)
    {
        public required DbSet<AggregatedPrice> AggregatePrices { get; set; }
    }
}
