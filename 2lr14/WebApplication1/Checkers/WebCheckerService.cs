namespace WebApplication1.Checkers
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class WebCheckerService : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WebCheckerService> _logger;
        private const string UrlToCheck = "https://www.example.com";
        private const string LogFilePath = "webcheck.log";

        public WebCheckerService(HttpClient httpClient, ILogger<WebCheckerService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(UrlToCheck, stoppingToken);
                    string logMessage = $"{DateTime.Now}: {UrlToCheck} is {(response.IsSuccessStatusCode ? "reachable" : "not reachable")}";
                    this._logger.LogInformation(logMessage);
                    File.AppendAllLines(LogFilePath, new[] { logMessage });
                }
                catch (Exception ex)
                {
                    string logMessage = $"{DateTime.Now}: Error checking {UrlToCheck} - {ex.Message}";
                    this._logger.LogError(logMessage);
                    File.AppendAllLines(LogFilePath, new[] { logMessage });
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }
    }
}
