using Common.Tpi.Models;

namespace Common.Test.Tpi.Models;

public class DataTest
{
    [Fact]
    public void StringRepresentation_ShouldBeValid_WithPartitionAndNoZone()
    {
        var sut = new Data("TEST", 1);

        var expected = "1TEST";

        var actual = sut.ToString();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void StringRepresentation_ShouldBeValid_WithZoneAndNoPartition()
    {
        var sut = new Data("TEST", null, "003");

        var expected = "003TEST";

        var actual = sut.ToString();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void StringRepresentation_ShouldBeValid_WithPartitionAndZone()
    {
        var sut = new Data("TEST", 1, "002");

        var expected = "1002TEST";

        var actual = sut.ToString();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void StringRepresentation_ShouldBeValid_WithoutPartitionOrZone()
    {
        var sut = new Data("TEST");

        var expected = "TEST";

        var actual = sut.ToString();

        Assert.Equal(expected, actual);
    }
}
