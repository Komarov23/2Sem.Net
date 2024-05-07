using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;

namespace WebApplication1.Checkers
{
    public class DatabaseWatcherService : BackgroundService
    {
        private readonly ILogger<DatabaseWatcherService> _logger;
        private readonly IDB _dbContext;
        private int _lastRecordId = 0;

        public DatabaseWatcherService(ILogger<DatabaseWatcherService> logger, IDB dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var latestRecord = await _dbContext.Students
                    .OrderByDescending(r => r.Id)
                    .FirstOrDefaultAsync();

                if (latestRecord != null && latestRecord.Id > _lastRecordId)
                {
                    _lastRecordId = latestRecord.Id;
                    Console.WriteLine($"New record with ID: {_lastRecordId}");
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
