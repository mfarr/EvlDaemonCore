namespace Common.Options;

public sealed record ConnectionOptions
{
    public required string Ip { get; set; }

    public required int Port { get; set; }

    public required string Password { get; set; }
}
