using Common.Tpi.Models;

namespace Common.Test.Tpi.Models;

public class PayloadTest
{
    [Fact]
    public void CalculatedChecksum_Should_BeCorrect()
    {
        var command = new Command("510");

        var data = new Data("8A");

        var sut = new Payload(command, data);

        var expected = "0F";

        Assert.Equal(expected, sut.Checksum);
    }

    [Fact]
    public void StringRepresentation_Should_BeInValidFormat()
    {
        var command = new Command("510");

        var data = new Data("8A");

        var sut = new Payload(command, data);

        var expected = "5108A0F";

        var actual = sut.ToString();

        Assert.Equal(expected, actual);
    }
}
