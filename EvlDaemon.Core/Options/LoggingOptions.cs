namespace EvlDaemon.Core.Options;

public enum LoggingLevel
{
    Error,
    Debug,
    Trace
}

public sealed record LoggingOptions
{
    public required string Name { get; set; }

    public required string Type { get; set; }

    public required LoggingLevel Level { get; set; }
}
