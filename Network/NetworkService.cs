using Common.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Network;

public class NetworkService : IHostedService
{
    private readonly IOptions<ConnectionOptions> _options;

    private readonly ILogger<NetworkService> _logger;
    
    public NetworkService(IOptions<ConnectionOptions> options, ILogger<NetworkService> logger)
    {
        _options = options;

        _logger = logger;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Starting network service");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}