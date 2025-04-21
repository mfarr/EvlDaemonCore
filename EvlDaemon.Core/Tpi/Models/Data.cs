namespace EvlDaemon.Core.Tpi.Models;

/// <summary>
///     Represents a data structure that combines a string value with optional partition and zone information.
/// </summary>
/// <param name="Value">The value representing the main data payload.</param>
/// <param name="Partition">An optional partition identifier.</param>
/// <param name="Zone">An optional zone identifier.</param>
public record Data(string Value, int? Partition = null, string? Zone = null)
{
    public override string ToString()
    {
        return $"{Partition}{Zone}{Value}";
    }
}
