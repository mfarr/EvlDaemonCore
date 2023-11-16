using System.Net;
using System.Net.Sockets;
using Common.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Network;

public sealed class NetworkEvlClient : IDisposable, IEvlClient
{
    public readonly int Port;

    public readonly IPAddress IpAddress;

    private readonly TcpClient _tcpClient;

    private readonly ILogger<NetworkEvlClient> _logger;

    private const int BufferSize = 1024;
    
    public NetworkEvlClient(IOptions<ConnectionOptions> connectionOptions, ILogger<NetworkEvlClient> logger)
    {
        Port = connectionOptions.Value.Port;

        if (!IPAddress.TryParse(connectionOptions.Value.Ip, out var ipAddress))
        {
            throw new ConfigurationException($"Invalid IP address format: {connectionOptions.Value.Ip}");
        }

        IpAddress = ipAddress;

        _tcpClient = new TcpClient();

        _logger = logger;
    }

    public void Connect()
    {
        if (_tcpClient.Connected)
        {
            throw new InvalidOperationException("EvlClient is already connected.");
        }
        
        _tcpClient.Connect(IpAddress, Port);
    }

    public async Task ListenForEventsAsync(CancellationToken ct)
    {
        if (!_tcpClient.Connected)
        {
            throw new InvalidOperationException("EvlClient must be connected before it can listen for events.");
        }

        var buffer = new byte[BufferSize];

        var stream = _tcpClient.GetStream();

        var bytesRead = await stream.ReadAsync(buffer.AsMemory(0, BufferSize), ct);

        while (bytesRead > 0 && !ct.IsCancellationRequested)
        {
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(buffer));

            try
            {
                bytesRead = await stream.ReadAsync(buffer.AsMemory(0, BufferSize), ct);
            }
            catch (OperationCanceledException e)
            {
                _logger.LogDebug(e, "Forcibly cancelling network read...");
            }
            catch (IOException e) when (e.InnerException is SocketException)
            {
                _logger.LogDebug("Connection aborted, stopping event listener...");

                // TODO: Create & use linked cancellation token?
                break;
            }
        }
    }

    public void Disconnect()
    {
        if (_tcpClient is {Connected: true})
        {
            _logger.LogDebug("Disconnecting from EVL device...");
            
            _tcpClient.Close();
        }
    }

    public void Dispose()
    {
        _logger.LogDebug("Disposing NetworkEvlClient...");
        
        Disconnect();
    }
}