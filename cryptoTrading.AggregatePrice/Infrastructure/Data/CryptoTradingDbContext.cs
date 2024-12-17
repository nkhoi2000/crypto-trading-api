using cryptoTrading.AggregatePrice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace cryptoTrading.AggregatePrice.Infrastructure.Data
{
    public class CryptoTradingDbContext : DbContext
    {
        public DbSet<AggregatedPrice> AggregatePrices { get; set; }
        public CryptoTradingDbContext(DbContextOptions<CryptoTradingDbContext> options) : base(options) { }

    }
}
