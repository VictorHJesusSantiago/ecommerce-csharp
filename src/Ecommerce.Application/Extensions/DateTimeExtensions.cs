namespace Ecommerce.Application.Extensions;

public static class DateTimeExtensions
{
    public static string ToRelativeTime(this DateTime dateTime)
    {
        var span = DateTime.UtcNow - dateTime;

        if (span.TotalMinutes < 1) return "just now";
        if (span.TotalMinutes < 60) return $"{(int)span.TotalMinutes} minutes ago";
        if (span.TotalHours < 24) return $"{(int)span.TotalHours} hours ago";
        if (span.TotalDays < 7) return $"{(int)span.TotalDays} days ago";
        if (span.TotalDays < 30) return $"{(int)(span.TotalDays / 7)} weeks ago";
        if (span.TotalDays < 365) return $"{(int)(span.TotalDays / 30)} months ago";
        return $"{(int)(span.TotalDays / 365)} years ago";
    }

    public static string ToShortDateString(this DateTime dateTime)
    {
        return dateTime.ToString("MMM dd, yyyy");
    }

    public static string ToFullDateString(this DateTime dateTime)
    {
        return dateTime.ToString("MMMM dd, yyyy");
    }

    public static string ToDateTimeString(this DateTime dateTime)
    {
        return dateTime.ToString("MMM dd, yyyy HH:mm");
    }

    public static bool IsToday(this DateTime dateTime)
    {
        return dateTime.Date == DateTime.UtcNow.Date;
    }

    public static bool IsYesterday(this DateTime dateTime)
    {
        return dateTime.Date == DateTime.UtcNow.Date.AddDays(-1);
    }

    public static bool IsThisWeek(this DateTime dateTime)
    {
        var now = DateTime.UtcNow;
        var startOfWeek = now.AddDays(-(int)now.DayOfWeek);
        return dateTime >= startOfWeek && dateTime <= now;
    }

    public static bool IsThisMonth(this DateTime dateTime)
    {
        var now = DateTime.UtcNow;
        return dateTime.Year == now.Year && dateTime.Month == now.Month;
    }

    public static DateTime StartOfDay(this DateTime dateTime)
    {
        return dateTime.Date;
    }

    public static DateTime EndOfDay(this DateTime dateTime)
    {
        return dateTime.Date.AddDays(1).AddTicks(-1);
    }

    public static DateTime StartOfWeek(this DateTime dateTime)
    {
        var diff = (7 + (dateTime.DayOfWeek - System.DayOfWeek.Monday)) % 7;
        return dateTime.AddDays(-1 * diff).Date;
    }

    public static DateTime StartOfMonth(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, 1);
    }

    public static DateTime EndOfMonth(this DateTime dateTime)
    {
        return dateTime.AddDays(1 - dateTime.Day).AddMonths(1).AddDays(-1);
    }

    public static int DaysInMonth(this DateTime dateTime)
    {
        return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
    }
}

public static class DecimalExtensions
{
    public static string ToCurrencyString(this decimal value, string symbol = "$")
    {
        return $"{symbol}{value:N2}";
    }

    public static string ToPercentageString(this decimal value)
    {
        return $"{value:P1}";
    }

    public static decimal RoundTo2Decimal(this decimal value)
    {
        return Math.Round(value, 2);
    }

    public static decimal CeilingToNearest(this decimal value, decimal nearest)
    {
        return Math.Ceiling(value / nearest) * nearest;
    }

    public static decimal FloorToNearest(this decimal value, decimal nearest)
    {
        return Math.Floor(value / nearest) * nearest;
    }

    public static bool IsBetween(this decimal value, decimal min, decimal max)
    {
        return value >= min && value <= max;
    }
}

public static class GuidExtensions
{
    public static string ToShortString(this Guid guid)
    {
        return guid.ToString("N")[..8];
    }

    public static bool IsEmpty(this Guid? guid)
    {
        return guid is null || guid == Guid.Empty;
    }

    public static bool IsValid(this Guid guid)
    {
        return guid != Guid.Empty;
    }
}
