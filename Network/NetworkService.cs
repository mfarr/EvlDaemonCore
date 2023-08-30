using System.Net;
using Common.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Network;

public class NetworkService : IHostedService
{
    private readonly ILogger<NetworkService> _logger;

    private readonly EvlClient _evlClient;
    
    public NetworkService(IOptions<ConnectionOptions> options, ILogger<NetworkService> logger)
    {
        if (!IPAddress.TryParse(options.Value.Ip, out var ipAddress))
        {
            throw new ConfigurationException($"Invalid IP address format: {options.Value.Ip}");
        }

        _evlClient = new EvlClient(options.Value.Port, ipAddress);

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
        _logger.LogDebug("Closing connection to {IpAddress} on {Port}", _evlClient.IpAddress, _evlClient.Port); 
        
        _evlClient.Disconnect();
        
        return Task.CompletedTask;
    }
}