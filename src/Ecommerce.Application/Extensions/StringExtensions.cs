namespace Ecommerce.Application.Extensions;

public static class StringExtensions
{
    public static string ToSlug(this string input)
    {
        return input.ToLowerInvariant()
            .Replace(" ", "-")
            .Replace("'", "")
            .Replace("\"", "")
            .Replace("&", "and")
            .Replace(",", "")
            .Replace(".", "")
            .Replace("!", "")
            .Replace("?", "")
            .Replace("@", "at")
            .Replace("#", "")
            .Replace("$", "")
            .Replace("%", "")
            .Replace("^", "")
            .Replace("*", "")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("[", "")
            .Replace("]", "")
            .Replace("{", "")
            .Replace("}", "")
            .Replace("|", "")
            .Replace("\\", "")
            .Replace("/", "")
            .Replace("+", "plus")
            .Replace("=", "equals")
            .Replace("<", "")
            .Replace(">", "")
            .Replace(",", "")
            .Replace(";", "")
            .Replace(":", "")
            .Replace("'", "")
            .Replace("`", "")
            .Replace("~", "")
            .Replace("  ", "-")
            .Trim('-');
    }

    public static string Truncate(this string input, int maxLength, string suffix = "...")
    {
        if (string.IsNullOrEmpty(input) || input.Length <= maxLength)
            return input ?? string.Empty;

        return input[..(maxLength - suffix.Length)] + suffix;
    }

    public static string ToTitleCase(this string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
    }

    public static string ToCamelCase(this string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        var words = input.Split(' ', '-', '_');
        return string.Join("", words.Select((w, i) => i == 0 ? w.ToLower() : char.ToUpper(w[0]) + w[1..].ToLower()));
    }

    public static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        return string.Concat(input.Select((c, i) => i > 0 && char.IsUpper(c) ? "_" + char.ToLower(c) : char.ToLower(c).ToString()));
    }

    public static string RemoveDiacritics(this string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        var normalized = input.Normalize(System.Text.NormalizationForm.FormD);
        var builder = new System.Text.StringBuilder();
        foreach (var c in normalized)
        {
            var category = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
            if (category != System.Globalization.UnicodeCategory.NonSpacingMark)
                builder.Append(c);
        }
        return builder.ToString().Normalize(System.Text.NormalizationForm.FormC);
    }

    public static bool ContainsIgnoreCase(this string source, string value)
    {
        return source?.Contains(value, StringComparison.OrdinalIgnoreCase) ?? false;
    }

    public static string OrDefault(this string? value, string defaultValue)
    {
        return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
    }

    public static string ToCurrencyFormat(this decimal value, string currencySymbol = "$")
    {
        return $"{currencySymbol}{value:N2}";
    }

    public static string ToFileSize(this long bytes)
    {
        return bytes switch
        {
            < 1024 => $"{bytes} B",
            < 1048576 => $"{bytes / 1024.0:F1} KB",
            < 1073741824 => $"{bytes / 1048576.0:F1} MB",
            _ => $"{bytes / 1073741824.0:F2} GB"
        };
    }
}
