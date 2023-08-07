#region

using Common.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

#endregion

namespace CommandLine;

public class ConsoleHost : IHostedService
{
    private ConnectionOptions _connectionOptions;
    private LoggingOptions _loggingOptions;

    public ConsoleHost(IOptions<ConnectionOptions> connectionOptions, IOptions<LoggingOptions> loggingOptions)
    {
        _connectionOptions = connectionOptions.Value;
        _loggingOptions = loggingOptions.Value;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Welcome to EvlDaemon.");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
