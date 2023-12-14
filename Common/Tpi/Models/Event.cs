namespace Common.Tpi.Models;

public class Event(Command command, Data? data = null)
{
    public Command Command { get; set; } = command;

    public Data? Data { get; set; } = data;
}
