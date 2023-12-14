namespace Common.Tpi.Models;

public class Payload
{
    public Command Command { get; }

    public Data? Data { get; }

    public string Checksum { get; }

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
