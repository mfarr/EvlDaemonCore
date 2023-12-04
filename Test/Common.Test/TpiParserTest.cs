namespace Common.Test;

public class TpiParserTest
{
    [Theory]
    [InlineData("6543", "D2")]
    [InlineData("5108A", "0F")]
    [InlineData("005123456", "CA")]
    public void CalculateChecksum_ShouldCalculateChecksum(string input, string expected)
    {
        var actual = TpiParser.CalculateChecksum(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseChecksum_ShouldReturnChecksumString_WithValidInput()
    {
        const string input = "5053CD";

        const string expected = "CD";

        var actual = TpiParser.ParseChecksum(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseChecksum_ShouldThrowException_WithInvalidInput()
    {
        const string input = "505";

        Assert.Throws<ArgumentException>(() => TpiParser.ParseChecksum(input));
    }

    [Fact]
    public void ParseCommand_ShouldReturnCommandString_WithValidInput()
    {
        const string input = "5053CD";

        const string expected = "505";

        var actual = TpiParser.ParseCommand(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseCommand_ShouldThrowException_WithInvalidInput()
    {
        const string input = "CD";

        Assert.Throws<ArgumentException>(() => TpiParser.ParseCommand(input));
    }
}
