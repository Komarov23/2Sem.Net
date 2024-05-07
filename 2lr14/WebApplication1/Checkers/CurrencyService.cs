using Microsoft.Extensions.Caching.Memory;

namespace WebApplication1.Checkers
{
    public class CurrencyService : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CurrencyService> _logger;

        public CurrencyService(HttpClient httpClient, IMemoryCache memoryCache, ILogger<CurrencyService> logger)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string cacheKey = "CurrencyRates";
                if (!_memoryCache.TryGetValue(cacheKey, out string currencyData))
                {
                    var response = await _httpClient.GetStringAsync("https://api.exchangerate-api.com/v4/latest/USD");
                    _memoryCache.Set(cacheKey, response, TimeSpan.FromMinutes(10));
                    _logger.LogInformation("Fetched and cached currency rates.");
                }
                else
                {
                    _logger.LogInformation("Using cached currency rates.");
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }
    }
}
