using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace System;

public class ConsoleHost : IHostedService
{
    private readonly ILogger<ConsoleHost> _logger;

    public ConsoleHost(ILogger<ConsoleHost> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Welcome to EvlDaemon");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Stopping EvlDaemon...");

        return Task.CompletedTask;
    }
}
