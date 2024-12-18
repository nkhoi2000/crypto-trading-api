using cryptoTrading.AggregatePrice.Application.Interfaces;
using cryptoTrading.AggregatePrice.Application.Services;
using cryptoTrading.AggregatePrice.Infrastructure.Data;
using cryptoTrading.AggregatePrice.Infrastructure.ExternalApis;
using Microsoft.EntityFrameworkCore;

namespace cryptoTrading.AggregatePrice.Infrastructure.DependencyInjection
{
    public static class ServiceRegister
    {
        public static void AddServiceRegisterLayer(this IServiceCollection services)
        {
            // Register DbContext as Scoped
            services.AddDbContext<CryptoTradingDbContext>(options =>
                options.UseInMemoryDatabase("TradingDb"));

            // Register sources
            services.AddScoped<IBinanceSource, BinancePriceSource>();
            services.AddScoped<IHoubiSource, HoubiPriceSource>();

            // Register Service
            services.AddScoped<IPriceService, AggregatePriceService>();

            services.AddHostedService<FetchPriceScheduler>();

            // Add HttpClient
            services.AddHttpClient();
        }
    }

}
