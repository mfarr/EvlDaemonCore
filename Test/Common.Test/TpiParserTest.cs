namespace Common.Test;

public class TpiParserTest
{
    [Fact]
    public void ParseChecksum_ShouldReturnChecksumString_WithValidInput()
    {
        var input = "5053CD";

        var expected = "CD";

        var actual = TpiParser.ParseChecksum(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseChecksum_ShouldThrowException_WithInvalidInput()
    {
        var input = "505";

        Assert.Throws<ArgumentException>(() => TpiParser.ParseChecksum(input));
    }

    [Fact]
    public void ParseCommand_ShouldReturnCommandString_WithValidInput()
    {
        var input = "5053CD";

        var expected = "505";

        var actual = TpiParser.ParseCommand(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseCommand_ShouldThrowException_WithInvalidInput()
    {
        var input = "CD";

        Assert.Throws<ArgumentException>(() => TpiParser.ParseCommand(input));
    }
}
