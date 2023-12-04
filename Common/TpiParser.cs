namespace Common;

public static class TpiParser
{
    public const int CommandLength = 3;

    public const int ChecksumLength = 2;

    private static readonly string InputStringTooShortMessage =
        $"Input must be at least {CommandLength + ChecksumLength} characters.";

    public static string CalculateChecksum(string value)
    {
        var sum = value.Sum(c => c);

        sum &= 255;

        return sum.ToString("X2");
    }

    public static string ParseCommand(string input)
    {
        if (input.Length < CommandLength + ChecksumLength)
        {
            throw new ArgumentException(InputStringTooShortMessage, nameof(input));
        }

        return input[..CommandLength];
    }

    public static string ParseChecksum(string input)
    {
        if (input.Length < CommandLength + ChecksumLength)
        {
            throw new ArgumentException(InputStringTooShortMessage, nameof(input));
        }

        return input[^ChecksumLength..];
    }

    /// <summary>
    /// Validates that <paramref name="input"/> is a properly formatted TPI command string with a valid checksum.
    /// </summary>
    /// <param name="input">String to validate</param>
    /// <returns>True, if the string is a properly formatted TPI command string with a valid checksum</returns>
    public static bool Validate(string input)
    {
        if (input.Length < CommandLength + ChecksumLength)
        {
            return false;
        }

        var checksum = ParseChecksum(input);

        var value = input[..^ChecksumLength];

        var calculated = CalculateChecksum(value);

        return checksum == calculated;
    }
}
