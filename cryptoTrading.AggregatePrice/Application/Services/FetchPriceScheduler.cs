using cryptoTrading.AggregatePrice.Application.Interfaces;

namespace cryptoTrading.AggregatePrice.Application.Services
{
    public class FetchPriceScheduler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public FetchPriceScheduler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var priceService = scope.ServiceProvider.GetRequiredService<IPriceService>();

                try
                {
                    await priceService.FetchAndAggregatePrices();
                }
                catch (Exception ex)
                {
                    // Log the exception (use ILogger if needed)
                    Console.WriteLine($"Error in FetchPriceScheduler: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
