namespace Common.Tpi.Models;

public class Data(string value, int? partition = null, string? zone = null)
{
    /// <summary>
    /// Partition data, if present
    /// </summary>
    public int? Partition { get; } = partition;

    /// <summary>
    /// Zone data, if present
    /// </summary>
    public string? Zone { get; } = zone;

    /// <summary>
    /// Value of data, if any, excluding partition and zone.
    /// </summary>
    public string Value { get; } = value;

    public override string ToString()
    {
        return $"{Partition}{Zone}{Value}";
    }
}
