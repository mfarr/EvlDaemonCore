namespace Common;

public static class TpiParser
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
