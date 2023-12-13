using System.Net;
using System.Net.Sockets;
using Common.Exceptions;
using Common.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Network;

public sealed class NetworkEvlClient : IEvlClient
{
    private readonly int _port;

    private readonly IPAddress _ipAddress;

    private readonly TcpClient _tcpClient;

    private readonly ILogger<NetworkEvlClient> _logger;

    private bool _listening;

    private const int BufferSize = 1024;

    private const string Terminator = "\r\n";

    private readonly CancellationTokenSource _cancellationTokenSource;

    public NetworkEvlClient(IOptions<ConnectionOptions> connectionOptions, ILogger<NetworkEvlClient> logger)
    {
        _port = connectionOptions.Value.Port;

        if (!IPAddress.TryParse(connectionOptions.Value.Ip, out var ipAddress))
        {
            throw new ConfigurationException($"Invalid IP address format: {connectionOptions.Value.Ip}.");
        }

        _ipAddress = ipAddress;

        _tcpClient = new TcpClient();

        _logger = logger;

        _listening = false;

        _cancellationTokenSource = new CancellationTokenSource();
    }

    public void Connect()
    {
        if (_tcpClient.Connected)
        {
            throw new InvalidOperationException("EvlClient is already connected.");
        }

        _tcpClient.Connect(_ipAddress, _port);
    }

    public async Task ListenForEventsAsync(CancellationToken externalCancellationToken)
    {
        if (!_tcpClient.Connected)
        {
            throw new InvalidOperationException("EvlClient must be connected before it can listen for events.");
        }

        if (_listening)
        {
            throw new InvalidOperationException("EvlClient is already listening for events.");
        }

        _listening = true;

        var internalCancellationToken = _cancellationTokenSource.Token;

        using var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(internalCancellationToken, externalCancellationToken);

        var linkedToken = linkedTokenSource.Token;

        var buffer = new byte[BufferSize];

        var incoming = "";

        var stream = _tcpClient.GetStream();

        while (true)
        {
            int bytesRead;

            try
            {
               bytesRead = await stream.ReadAsync(buffer.AsMemory(0, BufferSize), linkedToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogDebug("Cancellation requested");

                throw;
            }

            if (bytesRead <= 0)
            {
                throw new DeviceDisconnectException("Disconnected from EVL device.");
            }

            incoming += System.Text.Encoding.UTF8.GetString(buffer[..bytesRead]);

            var payloads = incoming.Split(Terminator).ToList();

            if (!incoming.EndsWith(Terminator))
            {
                incoming = payloads[^1];

                HandlePayloads(payloads[..^2]);
            }
            else
            {
                HandlePayloads(payloads[..^1]);
            }
        }
    }

    private void HandlePayloads(IEnumerable<string> payloads)
    {
        foreach (var payload in payloads)
        {
            _logger.LogTrace("Payload: {Payload}", payload);
        }
    }

    public void Disconnect()
    {
        if (_tcpClient is not {Connected: true})
        {
            return;
        }

        _logger.LogDebug("Closing connection to EVL device");

        _cancellationTokenSource.Cancel();

        _tcpClient.Close();

        _listening = false;
    }

    public void Dispose()
    {
        _logger.LogDebug("Disposing NetworkEvlClient");

        Disconnect();
    }
}
