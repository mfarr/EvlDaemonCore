namespace Common;

public static class TpiParser
{
    public const int CommandLength = 3;

    public const int ChecksumLength = 2;

    private static readonly string InputStringTooShortMessage =
        $"Input must be at least {CommandLength + ChecksumLength} characters.";

    public static bool ValidateChecksum(string input)
    {
        if (input.Length < CommandLength + ChecksumLength)
        {
            throw new ArgumentException(InputStringTooShortMessage, nameof(input));
        }

        return true;
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
}
