using System.Net;
using System.Net.Sockets;
using Common.Options;
using Microsoft.Extensions.Options;

namespace Network;

public sealed class NetworkEvlClient : IDisposable, IEvlClient
{
    public readonly int Port;

    public readonly IPAddress IpAddress;

    private readonly TcpClient _tcpClient;

    private const int BufferSize = 1024;
    
    public NetworkEvlClient(IOptions<ConnectionOptions> connectionOptions)
    {
        Port = connectionOptions.Value.Port;

        if (!IPAddress.TryParse(connectionOptions.Value.Ip, out var ipAddress))
        {
            throw new ConfigurationException($"Invalid IP address format: {connectionOptions.Value.Ip}");
        }

        IpAddress = ipAddress;

        _tcpClient = new TcpClient();
    }

    public void Connect()
    {
        if (_tcpClient.Connected)
        {
            throw new InvalidOperationException("EvlClient is already connected.");
        }
        
        _tcpClient.Connect(IpAddress, Port);
    }

    public async Task ListenForEventsAsync()
    {
        if (!_tcpClient.Connected)
        {
            throw new InvalidOperationException("EvlClient must be connected before it can listen for events.");
        }

        var buffer = new byte[BufferSize];

        var stream = _tcpClient.GetStream();

        var bytesRead = await stream.ReadAsync(buffer.AsMemory(0, BufferSize));

        while (bytesRead > 0)
        {
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(buffer));
            
            bytesRead = await stream.ReadAsync(buffer.AsMemory(0, BufferSize));
        }
    }

    public void Disconnect()
    {
        if (_tcpClient.Connected)
        {
            _tcpClient.Close();
        }
    }

    public void Dispose()
    {
        if (_tcpClient is {Connected: true})
        {
            _tcpClient.Close();
        }
    }
}