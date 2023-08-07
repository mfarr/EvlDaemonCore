namespace Common.Options;

public enum LoggingLevel
{
    Error,
    Debug,
    Trace
}

public sealed class LoggingOptions
{
    public required string Name { get; set; }

    public required string Type { get; set; }

    public required LoggingLevel Level { get; set; }
}

public sealed class ConnectionOptions
{
    public required string Ip { get; set; }

    public required int Port { get; set; }

    public required string Password { get; set; }
}
