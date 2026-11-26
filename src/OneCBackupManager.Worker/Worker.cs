using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OneCBackupManager.Worker;

public sealed class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("1C Backup Manager background service started. Backup engine is not enabled in version 0.1.0.");
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogDebug("Background service heartbeat.");
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
