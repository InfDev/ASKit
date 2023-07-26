using ASKit.Common.Extensions;
using System;
using Xunit;

namespace ASKit.Common.Tests;

public class DateTimeTests
{
    [Fact]
    public void BeginDay()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);
        var expectedValue = new DateTime(2019, 8, 13);
        var actual = value.BeginDay();
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void EndDay()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);
        var expectedValue = DateTime.Parse("2019-08-13 23:59:59.9999999");
        var actual = value.EndDay();
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void BeginMonth()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);
        var expectedValue = new DateTime(2019, 8, 1);
        var actual = value.BeginMonth();
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void EndMonth()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);
        var expectedValue = DateTime.Parse("2019-08-31 23:59:59.9999999");
        var actual = value.EndMonth();
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void DaysInMonth()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);
        var expectedValue = 31;
        var actual = value.DaysInMonth();
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void BeginQuarter()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);
        var expectedValue = new DateTime(2019, 7, 1);
        var actual = value.BeginQuarter();
        Assert.Equal(expectedValue, actual);

        value = new DateTime(2019, 2, 13, 14, 5, 45);
        expectedValue = new DateTime(2019, 1, 1);
        actual = value.BeginQuarter();
        Assert.Equal(expectedValue, actual);

        value = new DateTime(2019, 5, 13, 14, 5, 45);
        expectedValue = new DateTime(2019, 4, 1);
        actual = value.BeginQuarter();
        Assert.Equal(expectedValue, actual);

        value = new DateTime(2019, 12, 13, 14, 5, 45);
        expectedValue = new DateTime(2019, 10, 1);
        actual = value.BeginQuarter();
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void EndQuarter()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);
        var expectedValue = DateTime.Parse("2019-09-30 23:59:59.9999999");
        var actual = value.EndQuarter();
        Assert.Equal(expectedValue, actual);

        value = new DateTime(2019, 2, 13, 14, 5, 45);
        expectedValue = DateTime.Parse("2019-03-31 23:59:59.9999999");
        actual = value.EndQuarter();
        Assert.Equal(expectedValue, actual);

        value = new DateTime(2019, 5, 13, 14, 5, 45);
        expectedValue = DateTime.Parse("2019-06-30 23:59:59.9999999");
        actual = value.EndQuarter();
        Assert.Equal(expectedValue, actual);

        value = new DateTime(2019, 12, 13, 14, 5, 45);
        expectedValue = DateTime.Parse("2019-12-31 23:59:59.9999999");
        actual = value.EndQuarter();
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void BeginHalfYear()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);
        var expectedValue = new DateTime(2019, 7, 1);
        var actual = value.BeginHalfYear();
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void EndHalfYear()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);
        var expectedValue = DateTime.Parse("2019-12-31 23:59:59.9999999");
        var actual = value.EndHalfYear();
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void BeginYear()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);
        var expectedValue = new DateTime(2019, 1, 1);
        var actual = value.BeginYear();
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void EndYear()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);
        var expectedValue = DateTime.Parse("2019-12-31 23:59:59.9999999");
        var actual = value.EndYear();
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void Between()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);

        var expectedValue = true;
        var actual = value.Between(new DateTime(2019, 8, 13, 14, 5, 45), new DateTime(2019, 8, 13, 14, 5, 45));
        Assert.Equal(expectedValue, actual);

        expectedValue = false;
        actual = value.Between(new DateTime(2019, 8, 13, 14, 5, 43), new DateTime(2019, 8, 13, 14, 5, 44));
        Assert.Equal(expectedValue, actual);

        expectedValue = false;
        actual = value.Between(new DateTime(2019, 8, 13, 14, 5, 46), new DateTime(2019, 8, 13, 14, 5, 47));
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void SameDay()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);

        var expectedValue = true;
        var actual = value.SameDay(new DateTime(2019, 8, 13));
        Assert.Equal(expectedValue, actual);

        expectedValue = false;
        actual = value.SameDay(new DateTime(2019, 8, 14));
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void SameMonth()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);

        var expectedValue = true;
        var actual = value.SameMonth(new DateTime(2019, 8, 27));
        Assert.Equal(expectedValue, actual);

        expectedValue = false;
        actual = value.SameMonth(new DateTime(2019, 9, 27));
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void SameYear()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);

        var expectedValue = true;
        var actual = value.SameYear(new DateTime(2019, 10, 27));
        Assert.Equal(expectedValue, actual);

        expectedValue = false;
        actual = value.SameYear(new DateTime(2020, 9, 27));
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void ToYMD()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);
        var expectedValue = "2019.08.13";
        var actual = value.ToYMD();
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void ToYMDhms()
    {
        var value = new DateTime(2019, 8, 13, 14, 5, 45);
        var expectedValue = "2019.08.13 14:05:45";
        var actual = value.ToYMDhms();
        Assert.Equal(expectedValue, actual);
    }

    [Fact]
    public void DateTimeRange()
    {
        var date = new DateTime(2019, 8, 13, 14, 5, 45);
        DateTimeRange value = date.GetRange(TimePeriod.Day);
        var expectedValue1 = new DateTime(2019, 8, 13);
        var expectedValue2 = DateTime.Parse("2019-08-13 23:59:59.9999999");
        Assert.Equal(expectedValue1, value.Begin);
        Assert.Equal(expectedValue2, value.End);

        value = date.GetRange(TimePeriod.Month);
        expectedValue1 = new DateTime(2019, 8, 1);
        expectedValue2 = DateTime.Parse("2019-08-31 23:59:59.9999999");
        Assert.Equal(expectedValue1, value.Begin);
        Assert.Equal(expectedValue2, value.End);

        value = date.GetRange(TimePeriod.Quarter);
        expectedValue1 = new DateTime(2019, 7, 1);
        expectedValue2 = DateTime.Parse("2019-09-30 23:59:59.9999999");
        Assert.Equal(expectedValue1, value.Begin);
        Assert.Equal(expectedValue2, value.End);

        value = date.GetRange(TimePeriod.HalfYear);
        expectedValue1 = new DateTime(2019, 7, 1);
        expectedValue2 = DateTime.Parse("2019-12-31 23:59:59.9999999");
        Assert.Equal(expectedValue1, value.Begin);
        Assert.Equal(expectedValue2, value.End);

        value = date.GetRange(TimePeriod.Year);
        expectedValue1 = new DateTime(2019, 1, 1);
        expectedValue2 = DateTime.Parse("2019-12-31 23:59:59.9999999");
        Assert.Equal(expectedValue1, value.Begin);
        Assert.Equal(expectedValue2, value.End);
    }
}
