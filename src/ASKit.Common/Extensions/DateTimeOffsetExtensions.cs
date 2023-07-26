using System.Globalization;
using System.Runtime.CompilerServices;

namespace ASKit.Common.Extensions;

// Used:
// https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
// https://stackoverflow.com/questions/24245523/getting-the-first-and-last-day-of-a-month-using-a-given-datetime-object

/// <summary>
/// Extensions for DateTime
/// </summary>
public static class DateTimeOffsetExtensions
{
    /// <summary>
    /// The beginning of the day
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTimeOffset BeginDay(this DateTimeOffset value)
    {
        return value.Date;
    }

    /// <summary>
    /// End of the day, e.g. 17.05.2019 23:59:59.9999999
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTimeOffset EndDay(this DateTimeOffset value)
    {
        return value.Date.AddDays(1).AddTicks(-1);
    }

    /// <summary>
    /// The beginning of the month, e.g. 1.05.2019 00:00:00.000
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTimeOffset BeginMonth(this DateTimeOffset value)
    {
        return value.Date.AddDays(-(value.Date.Day - 1));
    }

    /// <summary>
    /// The end of the month, e.g. 31.05.2019 23:59:59.9999999
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTimeOffset EndMonth(this DateTimeOffset value)
    {
        return value.BeginMonth().AddMonths(1).AddTicks(-1);
    }

    /// <summary>
    /// Days in a month
    /// </summary>
    /// <param name="value">Any date of the month</param>
    /// <returns></returns>
    public static int DaysInMonth(this DateTimeOffset value)
    {
        return DateTime.DaysInMonth(value.Year, value.Month);
    }

    /// <summary>
    /// Quarter start
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTimeOffset BeginQuarter(this DateTimeOffset value)
    {
        var month = value.Month;
        if (month >= 10)
            month = 10;
        else if (month >= 7)
            month = 7;
        else if (month >= 4)
            month = 4;
        else
            month = 1;
        return new DateTime(value.Year, month, 1);
    }

    /// <summary>
    /// Quarter end
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTimeOffset EndQuarter(this DateTimeOffset value)
    {
        var month = value.Month;
        if (month >= 10)
            month = 12;
        else if (month >= 7)
            month = 9;
        else if (month >= 4)
            month = 6;
        else
            month = 3;
        return (new DateTime(value.Year, month, 1)).EndMonth();
    }

    /// <summary>
    /// The beginning of the half year
    /// </summary>
    /// <param name="anyPeriodDate"></param>
    /// <returns></returns>
    public static DateTimeOffset BeginHalfYear(this DateTimeOffset anyPeriodDate)
    {
        return new DateTimeOffset(new DateTime(anyPeriodDate.Year, anyPeriodDate.Month < 7 ? 1 : 7, 1));
    }

    /// <summary>
    /// The end of six months
    /// </summary>
    /// <param name="anyPeriodDate"></param>
    /// <returns></returns>
    public static DateTimeOffset EndHalfYear(this DateTimeOffset anyPeriodDate)
    {
        return (new DateTimeOffset(new DateTime(anyPeriodDate.Year, anyPeriodDate.Month < 7 ? 6 : 12, 1)).EndMonth());
    }

    /// <summary>
    /// The beginning of the year, e.g. 1.01.2019 00:00:00.000
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTimeOffset BeginYear(this DateTimeOffset value)
    {
        return new DateTimeOffset(new DateTime(value.Year, 1, 1));
    }

    /// <summary>
    /// The end of the year, e.g. 31.12.2019 23:59:59.9999999
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTimeOffset EndYear(this DateTimeOffset value)
    {
        return new DateTimeOffset(new DateTime(value.Year, 1, 1)).AddYears(1).AddTicks(-1);
    }

    /// <summary>
    /// Date in the specified range?
    /// </summary>
    /// <param name="value"></param>
    /// <param name="beginDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public static bool Between(this DateTimeOffset value, DateTimeOffset beginDate, DateTimeOffset endDate)
    {
        return value >= beginDate && value <= endDate;
    }

    /// <summary>
    /// In the same day?
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public static bool SameDay(this DateTimeOffset value1, DateTimeOffset value2)
    {
        return value1.Year == value2.Year && value1.Month == value2.Month && value1.Day == value2.Day && value1.Offset == value2.Offset;
    }

    /// <summary>
    /// In the same month?
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public static bool SameMonth(this DateTimeOffset value1, DateTimeOffset value2)
    {
        return value1.Year == value2.Year && value1.Month == value2.Month && value1.Offset == value2.Offset;
    }

    /// <summary>
    /// In the same year?
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public static bool SameYear(this DateTimeOffset value1, DateTimeOffset value2)
    {
        return value1.Year == value2.Year && value1.Offset == value2.Offset;
    }

    /// <summary>
    /// Returns the time range
    /// </summary>
    /// <param name="actualDate"></param>
    /// <param name="period"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static DateTimeOffsetRange GetRange(this DateTimeOffset actualDate, TimePeriod period)
    {
        return period switch {
            TimePeriod.Day => new DateTimeOffsetRange(actualDate.BeginDay(), actualDate.EndDay()),
            TimePeriod.Month => new DateTimeOffsetRange(actualDate.BeginMonth(), actualDate.EndMonth()),
            TimePeriod.Quarter => new DateTimeOffsetRange(actualDate.BeginQuarter(), actualDate.EndQuarter()),
            TimePeriod.HalfYear => new DateTimeOffsetRange(actualDate.BeginHalfYear(), actualDate.EndHalfYear()),
            TimePeriod.Year => new DateTimeOffsetRange(actualDate.BeginYear(), actualDate.EndYear()),
            _ => throw new ArgumentOutOfRangeException(nameof(period))
           };
    }

    /// <summary>
    /// Template formatting: yyyy.MM.dd
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToYMD(this DateTimeOffset value)
    {
        return value.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Template formatting: yyyy.MM.dd HH:mm:ss
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToYMDhms(this DateTimeOffset value)
    {
        return value.ToString("yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture);
    }
}
