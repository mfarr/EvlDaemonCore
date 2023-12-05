namespace Common;

public static class Tpi
{
    public const int CommandLength = 3;

    public const int ChecksumLength = 2;

    private static readonly string InputStringTooShortMessage =
        $"Input must be at least {CommandLength + ChecksumLength} characters.";

    /// <summary>
    /// Calculates the TPI compatible checksum of the <paramref name="input"/> string.
    /// </summary>
    /// <param name="input">The input string value</param>
    /// <returns>The calculated checksum value</returns>
    public static string CalculateChecksum(string input)
    {
        var sum = input.Sum(c => c);

        sum &= 255;

        return sum.ToString("X2");
    }

    /// <summary>
    /// Parses the checksum string from <paramref name="payload"/>, a properly formatted TPI payload string.
    /// </summary>
    /// <param name="payload">TPI payload string</param>
    /// <returns>The parsed checksum string</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="payload"/> is not a properly formatted TPI payload string</exception>
    public static string ParseChecksum(string payload)
    {
        if (payload.Length < CommandLength + ChecksumLength)
        {
            throw new ArgumentException(InputStringTooShortMessage, nameof(payload));
        }

        return payload[^ChecksumLength..];
    }

    /// <summary>
    /// Parses the command string from <paramref name="payload"/>, a properly formatted TPI payload string.
    /// </summary>
    /// <param name="payload">TPI payload string</param>
    /// <returns>The parsed command string</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="payload"/> is not a properly formatted TPI payload string</exception>
    public static string ParseCommand(string payload)
    {
        if (payload.Length < CommandLength + ChecksumLength)
        {
            throw new ArgumentException(InputStringTooShortMessage, nameof(payload));
        }

        return payload[..CommandLength];
    }

    /// <summary>
    /// Parses the partition number from a properly formatted TPI payload data string.
    /// </summary>
    /// <param name="data">TPI payload data string</param>
    /// <returns>The parsed partition number, or 0 if a value couldn't be parsed</returns>
    public static int ParsePartition(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            return 0;
        }

        return int.TryParse(data[0].ToString(), out var partition) ? partition : 0;
    }

    /// <summary>
    /// Parses the three character zone string from a properly formatted TPI payload data string.
    /// </summary>
    /// <param name="data">TPI payload data string</param>
    /// <returns>The parsed zone string, or an empty string if a value couldn't be parsed</returns>
    public static string ParseZone(string data)
    {
        return data.Length < 3 ? "" : data[..3];
    }

    /// <summary>
    /// Validates that <paramref name="payload"/> is a properly formatted TPI payload string with a valid checksum.
    /// </summary>
    /// <param name="payload">TPI payload string</param>
    /// <returns>True, if the string is a properly formatted TPI payload string with a valid checksum</returns>
    public static bool Validate(string payload)
    {
        if (payload.Length < CommandLength + ChecksumLength)
        {
            return false;
        }

        var checksum = ParseChecksum(payload);

        var value = payload[..^ChecksumLength];

        var calculated = CalculateChecksum(value);

        return checksum == calculated;
    }
}
