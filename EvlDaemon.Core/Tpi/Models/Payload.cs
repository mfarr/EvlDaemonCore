namespace EvlDaemon.Core.Tpi.Models;

public record Payload
{
    public Command Command { get; }

    public Data? Data { get; }

    public string Checksum { get; }

    /// <summary>
    ///     Represents a payload that consists of a command, optional data, and a calculated checksum.
    /// </summary>
    /// <param name="command">The command associated with the payload.</param>
    /// <param name="data">The optional data associated with the payload. Defaults to null.</param>
    public Payload(Command command, Data? data = null)
    {
        Command = command;

        Data = data;

        Checksum = Parser.CalculateChecksum($"{Command.Name}{Data}");
    }

    public override string ToString()
    {
        return $"{Command.Name}{Data}{Checksum}";
    }
}
