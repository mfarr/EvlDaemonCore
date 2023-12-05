namespace Common.Test.Tpi;

using Common.Tpi;

public class ParserTest
{
    [Theory]
    [InlineData("6543", "D2")]
    [InlineData("5108A", "0F")]
    [InlineData("005123456", "CA")]
    public void CalculateChecksum_ShouldCalculateChecksum(string input, string expected)
    {
        var actual = Parser.CalculateChecksum(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseChecksum_ShouldReturnChecksumString_WithValidInput()
    {
        const string input = "5053CD";

        const string expected = "CD";

        var actual = Parser.ParseChecksum(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseChecksum_ShouldThrowException_WithInvalidInput()
    {
        const string input = "505";

        Assert.Throws<ArgumentException>(() => Parser.ParseChecksum(input));
    }

    [Fact]
    public void ParseCommand_ShouldReturnCommandString_WithValidInput()
    {
        const string input = "5053CD";

        const string expected = "505";

        var actual = Parser.ParseCommand(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseCommand_ShouldThrowException_WithInvalidInput()
    {
        const string input = "CD";

        Assert.Throws<ArgumentException>(() => Parser.ParseCommand(input));
    }

    [Fact]
    public void ParsePartition_ShouldParsePartition_WithValidInput()
    {
        const string input = "304";

        const int expected = 3;

        var actual = Parser.ParsePartition(input);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid")]
    public void ParsePartition_ShouldReturnZero_WithInvalidInput(string input)
    {
        const int expected = 0;

        var actual = Parser.ParsePartition(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseZone_ShouldParseZone_WithValidInput()
    {
        const string input = "005ABC";

        const string expected = "005";

        var actual = Parser.ParseZone(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseZone_ShouldReturnEmptyString_WithInvalidInput()
    {
        const string input = "5A";

        const string expected = "";

        var actual = Parser.ParseZone(input);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("005user54", true)]
    [InlineData("005userAB", false)]
    [InlineData("AB", false)]
    public void Validate_ShouldCorrectlyValidateInput(string input, bool expected)
    {
        var actual = Parser.Validate(input);

        Assert.Equal(expected, actual);
    }
}
