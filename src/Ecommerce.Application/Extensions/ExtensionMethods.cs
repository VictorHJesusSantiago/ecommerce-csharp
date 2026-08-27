namespace Ecommerce.Application.Extensions;

public static class StringExtensions
{
    public static string ToSnakeCase(this string str)
    {
        return string.Concat(str.Select((x, i) =>
            i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
    }

    public static string ToCamelCase(this string str)
    {
        if (string.IsNullOrEmpty(str)) return str;
        var words = str.Split(new[] { '_', '-' }, StringSplitOptions.RemoveEmptyEntries);
        return string.Join("", words.Select((w, i) =>
            i == 0 ? w.ToLowerInvariant() :
            char.ToUpperInvariant(w[0]) + w[1..].ToLowerInvariant()));
    }

    public static string Truncate(this string str, int maxLength, string suffix = "...")
    {
        if (string.IsNullOrEmpty(str)) return str;
        if (str.Length <= maxLength) return str;
        return str[..(maxLength - suffix.Length)] + suffix;
    }

    public static string RemoveHtmlTags(this string str)
    {
        if (string.IsNullOrEmpty(str)) return str;
        return System.Text.RegularExpressions.Regex.Replace(str, "<.*?>", string.Empty);
    }

    public static string ToSlug(this string str)
    {
        if (string.IsNullOrEmpty(str)) return string.Empty;
        var slug = str.ToLowerInvariant()
            .Replace(" ", "-")
            .Replace("_", "-");
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\-]", "");
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"-{2,}", "-");
        return slug.Trim('-');
    }

    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch { return false; }
    }

    public static string FormatFileSize(this long bytes)
    {
        if (bytes < 1024) return $"{bytes} B";
        if (bytes < 1024 * 1024) return $"{bytes / 1024.0:F1} KB";
        if (bytes < 1024 * 1024 * 1024) return $"{bytes / (1024.0 * 1024.0):F1} MB";
        return $"{bytes / (1024.0 * 1024.0 * 1024.0):F2} GB";
    }

    public static string MaskEmail(this string email)
    {
        if (string.IsNullOrEmpty(email) || !email.Contains('@')) return email;
        var parts = email.Split('@');
        var name = parts[0];
        var domain = parts[1];
        if (name.Length <= 2) return $"**@{domain}";
        return $"{name[0]}{new string('*', name.Length - 2)}{name[^1]}@{domain}";
    }
}

public static class CollectionExtensions
{
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    public static IEnumerable<T> OrderOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, bool descending)
    {
        return descending ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
    }

    public static PagedResult<T> ToPagedResult<T>(this IEnumerable<T> source, int totalCount, int page, int pageSize)
    {
        return new PagedResult<T>
        {
            Items = source.ToList(),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
    {
        return source == null || !source.Any();
    }

    public static List<T> Page<T>(this IList<T> source, int page, int pageSize)
    {
        return source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }
}

public static class DateTimeExtensions
{
    public static string ToRelativeTime(this DateTime dateTime)
    {
        var timeSpan = DateTime.UtcNow - dateTime;
        if (timeSpan.TotalSeconds < 60) return "just now";
        if (timeSpan.TotalMinutes < 60) return $"{(int)timeSpan.TotalMinutes} minute(s) ago";
        if (timeSpan.TotalHours < 24) return $"{(int)timeSpan.TotalHours} hour(s) ago";
        if (timeSpan.TotalDays < 30) return $"{(int)timeSpan.TotalDays} day(s) ago";
        if (timeSpan.TotalDays < 365) return $"{(int)(timeSpan.TotalDays / 30)} month(s) ago";
        return $"{(int)(timeSpan.TotalDays / 365)} year(s) ago";
    }

    public static string ToShortDateString(this DateTime dateTime)
    {
        return dateTime.ToString("MMM dd, yyyy");
    }

    public static string ToFullDateTimeString(this DateTime dateTime)
    {
        return dateTime.ToString("MMMM dd, yyyy HH:mm:ss");
    }

    public static bool IsBusinessDay(this DateTime date)
    {
        return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
    }

    public static int BusinessDaysUntil(this DateTime start, DateTime end)
    {
        var days = 0;
        var current = start;
        while (current < end)
        {
            if (current.IsBusinessDay()) days++;
            current = current.AddDays(1);
        }
        return days;
    }
}

public static class DecimalExtensions
{
    public static string ToCurrencyString(this decimal amount, string currencyCode = "USD")
    {
        return currencyCode switch
        {
            "USD" => amount.ToString("C", new System.Globalization.CultureInfo("en-US")),
            "EUR" => amount.ToString("C", new System.Globalization.CultureInfo("de-DE")),
            "GBP" => amount.ToString("C", new System.Globalization.CultureInfo("en-GB")),
            "JPY" => amount.ToString("C0", new System.Globalization.CultureInfo("ja-JP")),
            _ => $"{amount:F2} {currencyCode}"
        };
    }

    public static decimal RoundToTwoDecimals(this decimal value)
    {
        return Math.Round(value, 2);
    }

    public static decimal ToPercentage(this decimal value, decimal total)
    {
        if (total == 0) return 0;
        return Math.Round(value / total * 100, 2);
    }
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = [];
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrevious => Page > 1;
    public bool HasNext => Page < TotalPages;
}
