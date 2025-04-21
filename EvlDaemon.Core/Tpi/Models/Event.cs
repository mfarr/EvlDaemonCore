namespace EvlDaemon.Core.Tpi.Models;

/// <summary>
///     Represents an event that contains a command and optional data associated with it.
/// </summary>
/// <param name="Command">The command associated with the event.</param>
/// <param name="Data">The optional data associated with the event. Defaults to null.</param>
public record Event(Command Command, Data? Data = null);
