using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Network;

public class NetworkService : BackgroundService
{
    private readonly ILogger<NetworkService> _logger;

    private readonly IEvlClient _evlClient;
    
    public NetworkService(IEvlClient evlClient, ILogger<NetworkService> logger)
    {
        _evlClient = evlClient;

        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _evlClient.Connect();
        
        await _evlClient.ListenForEventsAsync(cancellationToken);
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Starting network service");

        return base.StartAsync(cancellationToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Stopping network service");
        
        _evlClient.Disconnect();
        
        return base.StopAsync(cancellationToken);
    }
}