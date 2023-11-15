using System.Net;
using Common.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Network;

public class NetworkService : IHostedService
{
    private readonly ILogger<NetworkService> _logger;

    private readonly IEvlClient _evlClient;
    
    public NetworkService(IEvlClient evlClient, ILogger<NetworkService> logger)
    {
        _evlClient = evlClient;

        _logger = logger;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Starting network service");

        _evlClient.Connect();

        await _evlClient.ListenForEventsAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Stopping network service");
        
        _evlClient.Disconnect();
        
        return Task.CompletedTask;
    }
}