namespace Common.Test;

public class TpiTest
{
    [Theory]
    [InlineData("6543", "D2")]
    [InlineData("5108A", "0F")]
    [InlineData("005123456", "CA")]
    public void CalculateChecksum_ShouldCalculateChecksum(string input, string expected)
    {
        var actual = Tpi.CalculateChecksum(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseChecksum_ShouldReturnChecksumString_WithValidInput()
    {
        const string input = "5053CD";

        const string expected = "CD";

        var actual = Tpi.ParseChecksum(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseChecksum_ShouldThrowException_WithInvalidInput()
    {
        const string input = "505";

        Assert.Throws<ArgumentException>(() => Tpi.ParseChecksum(input));
    }

    [Fact]
    public void ParseCommand_ShouldReturnCommandString_WithValidInput()
    {
        const string input = "5053CD";

        const string expected = "505";

        var actual = Tpi.ParseCommand(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseCommand_ShouldThrowException_WithInvalidInput()
    {
        const string input = "CD";

        Assert.Throws<ArgumentException>(() => Tpi.ParseCommand(input));
    }

    [Fact]
    public void ParsePartition_ShouldParsePartition_WithValidInput()
    {
        const string input = "304";

        const int expected = 3;

        var actual = Tpi.ParsePartition(input);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid")]
    public void ParsePartition_ShouldReturnZero_WithInvalidInput(string input)
    {
        const int expected = 0;

        var actual = Tpi.ParsePartition(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseZone_ShouldParseZone_WithValidInput()
    {
        const string input = "005ABC";

        const string expected = "005";

        var actual = Tpi.ParseZone(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParseZone_ShouldReturnEmptyString_WithInvalidInput()
    {
        const string input = "5A";

        const string expected = "";

        var actual = Tpi.ParseZone(input);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("005user54", true)]
    [InlineData("005userAB", false)]
    [InlineData("AB", false)]
    public void Validate_ShouldCorrectlyValidateInput(string input, bool expected)
    {
        var actual = Tpi.Validate(input);

        Assert.Equal(expected, actual);
    }
}
