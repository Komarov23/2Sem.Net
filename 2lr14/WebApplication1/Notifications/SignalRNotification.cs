using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class NotificationHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}

public class SignalRNotificationService : BackgroundService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly ILogger<SignalRNotificationService> _logger;

    public SignalRNotificationService(IHubContext<NotificationHub> hubContext, ILogger<SignalRNotificationService> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int messageCount = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            messageCount++;
            var message = $"Новина {messageCount}";
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Сервер", message);
            _logger.LogInformation($"Відправлено повідомлення {messageCount} до всіх клієнтів.");

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}