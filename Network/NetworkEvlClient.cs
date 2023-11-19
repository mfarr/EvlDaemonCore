﻿using System.Net;
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

    private readonly CancellationTokenSource _cancellationTokenSource;
    
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

        _cancellationTokenSource = new CancellationTokenSource();
        
    }

    public void Connect()
    {
        if (_tcpClient.Connected)
        {
            throw new InvalidOperationException("EvlClient is already connected.");
        }
        
        _tcpClient.Connect(IpAddress, Port);
    }

    public async Task ListenForEventsAsync(CancellationToken externalCancellationToken)
    {
        if (!_tcpClient.Connected)
        {
            throw new InvalidOperationException("EvlClient must be connected before it can listen for events.");
        }

        // TODO: Add check for already listening

        var internalCancellationToken = _cancellationTokenSource.Token;

        using var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(internalCancellationToken, externalCancellationToken);

        var linkedToken = linkedTokenSource.Token;

        var buffer = new byte[BufferSize];

        var stream = _tcpClient.GetStream();

        var bytesRead = await stream.ReadAsync(buffer.AsMemory(0, BufferSize), linkedToken);

        while (bytesRead > 0 && !linkedToken.IsCancellationRequested)
        {
            _logger.LogTrace("Received: {Data} ", System.Text.Encoding.UTF8.GetString(buffer));

            // TODO: Parse data

            try
            {
                bytesRead = await stream.ReadAsync(buffer.AsMemory(0, BufferSize), linkedToken);
            }
            catch (OperationCanceledException e)
            {
                _logger.LogDebug("Cancellation requested...");
            }
        }
    }

    public void Disconnect()
    {
        if (_tcpClient is {Connected: true})
        {
            _logger.LogDebug("Disconnecting from EVL device...");

            _cancellationTokenSource.Cancel();
            
            _tcpClient.Close();
        }
    }

    public void Dispose()
    {
        _logger.LogDebug("Disposing NetworkEvlClient...");
        
        Disconnect();
    }
}