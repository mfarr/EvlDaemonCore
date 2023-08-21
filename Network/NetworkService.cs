using System.Net;
using System.Net.Sockets;
using Common.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Network;

public class NetworkService : IHostedService
{
    private readonly ConnectionOptions _options;

    private readonly ILogger<NetworkService> _logger;

    private readonly TcpClient _tcpClient;
    
    public NetworkService(IOptions<ConnectionOptions> options, ILogger<NetworkService> logger)
    {
        _options = options.Value;

        _logger = logger;

        _tcpClient = new TcpClient();
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Starting network service");
        
        ConnectToDevice();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        if (_tcpClient.Connected)
        {
            _logger.LogDebug("Closing connection to {IpAddress} on {Port}", _options.Ip, _options.Port);
            _tcpClient.Close();
        }

        return Task.CompletedTask;
    }

    private void ConnectToDevice()
    {
        _logger.LogDebug("Connecting to {IpAddress} on {Port}", _options.Ip, _options.Port);
        
        if (_tcpClient.Connected)
        {
            throw new InvalidOperationException(
                $"Attempting to connect but {nameof(_tcpClient)} is already connected!");
        }

        if (!IPAddress.TryParse(_options.Ip, out var ipAddress))
        {
            throw new FormatException($"Invalid IP address format: {_options.Ip}");
        }

        _tcpClient.Connect(ipAddress, _options.Port);
        
        _logger.LogDebug("Connected to {IPAddress} on {Port}", _options.Ip, _options.Port);
    }
}